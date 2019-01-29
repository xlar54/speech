using System;
using System.Speech.Recognition;

namespace HelloSpeech
{
    class Program
    {
        static bool done = false;

        static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("I'm listening");

        //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-us");
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        sre.SetInputToDefaultAudioDevice();
        sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
        sre.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(sre_RecognizeCompleted);

        Choices colorChoices = new Choices();
        colorChoices.Add("red");       
        colorChoices.Add("green");        
        colorChoices.Add("blue");
        colorChoices.Add("orange");
        colorChoices.Add("yellow");
        colorChoices.Add("white");
        colorChoices.Add("black");
        colorChoices.Add("purple");
        colorChoices.Add("quit");

        GrammarBuilder colorsGrammarBuilder = new GrammarBuilder();
        colorsGrammarBuilder.Append(colorChoices);
        Grammar keyWordsGrammar = new Grammar(colorsGrammarBuilder);
        sre.LoadGrammarAsync(keyWordsGrammar);

        sre.RecognizeAsync(RecognizeMode.Multiple);

        while (done == false) { ; }

        Console.WriteLine("Done");
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.ReadLine();
      }
    } // Main

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "quit")
            {
                ((SpeechRecognitionEngine)sender).RecognizeAsyncCancel();
                return;
            }
            if (e.Result.Confidence >= 0.75)
                Console.WriteLine("I heard " + e.Result.Text);
            else
                Console.WriteLine("Unknown word");
        }

        static void sre_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            done = true;
        }
    } // class Program
} // ns