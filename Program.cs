using System;
using log4net.Config;
using NDesk.Options;

namespace AmortizationCalculator
{
  class Program
  {
    static void Main(string[] args)
    {
      XmlConfigurator.Configure();
      var options = new OptionSet();
      var principal = 0.0d;
      var rate = 0.0d;
      var payments = 0;
      options.Add("p=|principal=", v => principal = double.Parse(v))
             .Add("r=|rate=", v => rate = double.Parse(v) / 100.0d)
             .Add("n=|payments=", v => payments = int.Parse(v));
      options.Parse(args);
      var calculator = new AmortizationLibrary.AmortizationCalculator();
      var amortizationSchedule = calculator.Calculate(principal, rate, payments);
      foreach(var detail in amortizationSchedule)
      {
        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
          detail.PaymentNumber,
          detail.Payment.ToString("C"),
          detail.InterestPaid.ToString("C"),
          detail.PrincipalPaid.ToString("C"),
          detail.Balance.ToString("C")
          );
      }
      Console.ReadLine();
    }
  }
}
