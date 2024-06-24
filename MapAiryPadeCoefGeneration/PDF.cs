namespace MapAiryPadeCoefGeneration {
    internal class PDF {
        static void Main_() {
            PDFPlus.Execute();
            PDFMinus.Execute();
            PDFMinusLimit.Execute();
            PDFPlusLimit.Execute();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
