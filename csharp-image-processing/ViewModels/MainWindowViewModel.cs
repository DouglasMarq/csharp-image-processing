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
        private int firstkp;
        private int secondkp;
        private int thirdkp;
        private int fourthkp;

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
                }
                //não encontrou nada
                return Task.FromResult("NotFound");
            }
        }

        public Task<string> AuthenticateBio(bool? check)
        {
            int matchesCounter = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                //arquivos da pasta images
                var files = Directory.GetFiles(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Resources\\Images");
                //Source -- arquivo que escolhi e transformo ele em cinza
                Mat src = new Mat(Path.GetFullPath(openFileDialog.FileName), ImreadModes.Grayscale);
                //SURF - Speeded Up Robust Features
                var detector = SURF.Create(hessianThreshold: 400);
                //variaveis criadas em run-time, garbage collector cuida deles depois.
                var imgMatches = new Mat();
                //aqui é o matcher -- COMPARADOR
                var matcher = new BFMatcher();

                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Mat resSrcTermination = new Mat();
                            Mat resDstTermination = new Mat();
                            //pega o src, dá resize e joga em resSrc
                            Cv2.Resize(src, resSrcTermination, new Size(450, 450));
                            // pega a área de interesse
                            var srcTerminacao = new Mat(resSrcTermination, new Rect(75, 75, 150, 150));
                            foreach (var item in files)
                            {
                                //arquivo destinatario
                                Mat dst = new Mat(item, ImreadModes.Grayscale);
                                //pega o dst, dá resize e joga em resDst
                                Cv2.Resize(dst, resDstTermination, new Size(450, 450));
                                //pega a área de interesse
                                var resTerminacao = new Mat(resDstTermination, new Rect(75, 75, 150, 150));
                                // Keypoints - são as bolinhas
                                var keypoints1 = detector.Detect(srcTerminacao);
                                var keypoints2 = detector.Detect(resTerminacao);
                                // --------------------
                                if (keypoints1.Length == keypoints2.Length)
                                {
                                    firstkp = keypoints1.Length;
                                    matchesCounter++;
                                    if (check ?? false)
                                    {
                                        //Match das imagens filtradas
                                        var matches = matcher.Match(srcTerminacao, resTerminacao);
                                        try
                                        {
                                            //desenha as linhas entre os keypoints
                                            Cv2.DrawMatches(srcTerminacao, keypoints1, resTerminacao, keypoints2, matches, imgMatches);
                                            //mostra os matches
                                            Cv2.ImShow("Terminação", imgMatches);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case 1:
                            Mat resSrcBifurcation = new Mat();
                            Mat resDstBifurcation = new Mat();
                            //pega o src, dá resize e joga em resSrc
                            Cv2.Resize(src, resSrcBifurcation, new Size(450, 450));
                            //
                            var srcBifurcacao = new Mat(resSrcBifurcation, new Rect(75, 250, 150, 150));
                            foreach (var item in files)
                            {
                                //arquivo destinatario
                                Mat dst = new Mat(item, ImreadModes.Grayscale);
                                //pega o dst, dá resize e joga em resDst
                                Cv2.Resize(dst, resDstBifurcation, new Size(450, 450));
                                //pega a área de interesse
                                var resBifurcacao = new Mat(resDstBifurcation, new Rect(75, 250, 150, 150));
                                // Keypoints - são as bolinhas
                                var keypoints1 = detector.Detect(srcBifurcacao);
                                var keypoints2 = detector.Detect(resBifurcacao);
                                // --------------------
                                if (keypoints1.Length == keypoints2.Length)
                                {
                                    matchesCounter++;
                                    secondkp = keypoints1.Length;
                                    if (check ?? false)
                                    {
                                        //Match das imagens filtradas
                                        var matches = matcher.Match(srcBifurcacao, resBifurcacao);
                                        try
                                        {
                                            //desenha as linhas entre os keypoints
                                            Cv2.DrawMatches(srcBifurcacao, keypoints1, resBifurcacao, keypoints2, matches, imgMatches);
                                            //mostra os matches
                                            Cv2.ImShow("Bifurcação", imgMatches);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case 2:
                            Mat resSrcIndependency = new Mat();
                            Mat resDstIndependency = new Mat();
                            //pega o src, dá resize e joga em resSrc
                            Cv2.Resize(src, resSrcIndependency, new Size(450, 450));
                            // pega a área de interesse
                            var srcIndependency = new Mat(resSrcIndependency, new Rect(235, 250, 150, 120));
                            foreach (var item in files)
                            {
                                //arquivo destinatario
                                Mat dst = new Mat(item, ImreadModes.Grayscale);
                                //pega o dst, dá resize e joga em resDst
                                Cv2.Resize(dst, resDstIndependency, new Size(450, 450));
                                //pega a área de interesse
                                var resIndependency = new Mat(resDstIndependency, new Rect(235, 250, 150, 120));
                                // Keypoints - são as bolinhas
                                var keypoints1 = detector.Detect(srcIndependency);
                                var keypoints2 = detector.Detect(resIndependency);
                                // --------------------
                                if (keypoints1.Length == keypoints2.Length)
                                {
                                    thirdkp = keypoints1.Length;
                                    matchesCounter++;
                                    if (check ?? false)
                                    {
                                        //Match das imagens filtradas
                                        var matches = matcher.Match(srcIndependency, resIndependency);
                                        try
                                        {
                                            //desenha as linhas entre os keypoints
                                            Cv2.DrawMatches(srcIndependency, keypoints1, resIndependency, keypoints2, matches, imgMatches);
                                            //mostra os matches
                                            Cv2.ImShow("Independente", imgMatches);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        case 3:
                            Mat resSrcIsland = new Mat();
                            Mat resDstIsland = new Mat();
                            //pega o src, dá resize e joga em resSrc
                            Cv2.Resize(src, resSrcIsland, new Size(450, 450));
                            // pega a área de interesse
                            var srcIlha = new Mat(resSrcIsland, new Rect(220, 220, 150, 130));
                            foreach (var item in files)
                            {
                                //arquivo destinatario
                                Mat dst = new Mat(item, ImreadModes.Grayscale);
                                //pega o dst, dá resize e joga em resDst
                                Cv2.Resize(dst, resDstIsland, new Size(450, 450));
                                //pega a área de interesse
                                var resIlha = new Mat(resDstIsland, new Rect(220, 220, 150, 130));
                                // Keypoints - são as bolinhas
                                var keypoints1 = detector.Detect(srcIlha);
                                var keypoints2 = detector.Detect(resIlha);
                                // --------------------
                                if (keypoints1.Length == keypoints2.Length)
                                {
                                    fourthkp = keypoints1.Length;
                                    matchesCounter++;
                                    if (check ?? false)
                                    {
                                        //Match das imagens filtradas
                                        var matches = matcher.Match(srcIlha, resIlha);
                                        try
                                        {
                                            //desenha as linhas entre os keypoints
                                            Cv2.DrawMatches(srcIlha, keypoints1, resIlha, keypoints2, matches, imgMatches);
                                            //mostra os matches
                                            Cv2.ImShow("Ilha", imgMatches);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        default:
                            return Task.FromResult("Canceled");
                    }
                }
                if (matchesCounter == 4 && firstkp == 201 && secondkp == 169 && thirdkp == 127 && fourthkp == 143)
                {
                    return Task.FromResult("ADMIN");
                }
                else if (matchesCounter == 4 && firstkp == 174 && secondkp == 169 && thirdkp == 133 && fourthkp == 154)
                {
                    return Task.FromResult("DIRETOR");
                }
                else if (matchesCounter == 4)
                {
                    return Task.FromResult("OK");
                }
                else
                {
                    return Task.FromResult("Wrong");
                }
            }
            else
            {
                return Task.FromResult("Canceled");
            }
        }
    }
}
