namespace MapAiryPadeCoefGeneration {
    internal class CDF {
        static void Main_() {
            CDFPlus.Execute();
            CDFMinus.Execute();
            CDFMinusLimit.Execute();
            CDFPlusLimit.Execute();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
