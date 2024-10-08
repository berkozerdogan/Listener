using System;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace Listener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
            SpeechSynthesizer alice = new SpeechSynthesizer();
            SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
            Random rnd = new Random();
            int RecTimeOut = 0;
            Choices list = new Choices();
            list.Add(new string[] { "hello", "how are you", "what time is it" });
            engine.SetInputToDefaultAudioDevice();
            engine.LoadGrammarAsync(new Grammar(new GrammarBuilder(list)));
            engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
            engine.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(engine_SpeechRecognized);
            engine.RecognizeAsync(RecognizeMode.Multiple);

            //startlistening.SetInputToDefaultAudioDevice();
            //startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(list)));
            //startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_SpeechRecognized);
        }

        private static void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SpeechSynthesizer alice = new SpeechSynthesizer();
            int ranNum;
            string speech = e.Result.Text;

            if(speech == "Hello")
            {
                alice.SpeakAsync("Hello, I am here");
            }
            else if (speech == "How are you")
            {
                alice.SpeakAsync("I am fine Berk");
            }
            else if (speech == "What time is it")
            {
                alice.SpeakAsync(DateTime.Now.ToString("h mm tt"));
            }
            else
            {
                alice.SpeakAsync("Anlamadım");
            }
        }
        private static void engine_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            Console.WriteLine("Ses algılandı");
        }

        //private static void startlistening_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        //    string speech = e.Result.Text;
            
        //    if(speech == "Wake up")
        //    {
        //        startlistening.RecognizeAsyncCancel();
        //        alice.SpeakAsync("Yes, I am here");
        //        engine.RecognizeAsync(RecognizeMode.Multiple);
        //    }
        //}

    }
}