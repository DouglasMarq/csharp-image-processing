using csharp_image_processing.Model.Database;
using csharp_image_processing.Model.Entidades;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace csharp_image_processing.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Variables
        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        private string _pass;
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                SetProperty(ref _pass, value);
            }
        }
        #endregion

        //constructor
        public MainWindowViewModel()
        {
        }

        public Task<string> Authenticate(string user, string pass)
        {
            //verifica se está vazio
            if (string.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(pass))
            {
                return Task.FromResult("Form");
            }
            //verifica se está errado
            else if (string.IsNullOrWhiteSpace(pass))
            {
                return Task.FromResult("Wrong");
            }
            else //OK
            {
                //verifica usuario e senha
                var lg = new AccessLogin().SelectAll();
                foreach (var item in lg)
                {
                    if (item.User == user && item.Pass == pass)
                    {
                        return Task.FromResult("OK");
                    }
                    else
                    {
                        //não encontrou nada
                        return Task.FromResult("NotFound");
                    }
                }
                //não encontrou nada
                return Task.FromResult("NotFound");
            }
        }

        public Task<string> AuthenticateBio(bool? check)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                //pego o arquivo que selecionei
                var filePath = Path.GetFullPath(openFileDialog.FileName);                
                //arquivos da pasta images
                var files = Directory.GetFiles(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Resources\\Images");
                //SURF - Speeded Up Robust Features
                var detector = SURF.Create(hessianThreshold: 400);
                //Source -- arquivo que escolhi e transformo ele em cinza
                Mat src = new Mat(filePath, ImreadModes.Grayscale);
                Mat resSrc = new Mat();
                Cv2.Resize(src, resSrc, new Size(250, 250));
                //aqui é o matcher -- COMPARADOR
                var matcher = new BFMatcher();
                foreach (var item in files)
                {
                    //arquivo destinatario
                    Mat dst = new Mat(item, ImreadModes.Grayscale);
                    Mat resDst = new Mat();
                    Cv2.Resize(dst, resDst, new Size(250, 250));
                    // Keypoints - são as bolinhas
                    var keypoints1 = detector.Detect(resSrc);
                    var keypoints2 = detector.Detect(resDst);
                    // --------------------
                    if (keypoints1.Length == keypoints2.Length)
                    {
                        if (check ?? false)
                        {
                            var matches = matcher.Match(resSrc, resDst);
                            var imgMatches = new Mat();
                            //desenha as linhas entre os keypoints
                            Cv2.DrawMatches(resSrc, keypoints1, resDst, keypoints2, matches, imgMatches);
                            Cv2.ImShow("Matches", imgMatches);
                        }
                        return Task.FromResult("OK");
                    }
                }
                return Task.FromResult("Wrong");
            }
            else
            {
                return Task.FromResult("Canceled");
            }
        }
    }
}
