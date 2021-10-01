using System;

namespace Support_Bank
{
    using System.Collections.Generic;
    public class Account
    {

   public Account(string name)
   {
       m_Name = name;

 /*      m_Ingoings = ingoings;
       m_Outgoings = outgoings;
       
       m_totalIngoings = totalIngoings;
       m_totalOutgoings = totalOutgoings;
       m_accountBalance = accountBalance;
  */ }
   
   // Getter 
   
   // Determines and sets total account balance
   public float SetAccountBalance(float accountBalance)
   {
       m_accountBalance = m_totalIngoings - m_totalOutgoings;
       return accountBalance;
   }
       // Prints formatted account info to console

   public void GetAccountInfo(string accountInfo)
   {
       Console.WriteLine($"Account Name: {m_Name} \r\n Owing: {m_Outgoings} \r\n Owed: {m_Ingoings}");
       if (m_accountBalance < 0)
       {
           Console.WriteLine($"After settling, {m_Name} will owe {m_accountBalance}");
       }
       else if (m_accountBalance > 0)
       {
           Console.WriteLine($"After settling, {m_Name} will be owed {m_accountBalance}");
       }
       else
       {
           Console.WriteLine($"Hey, {m_Name} broke even!");
       }
   }
   
   // Members
   private List<Transaction> m_Ingoings;
   private List<Transaction> m_Outgoings;
   
   private string m_Name;
   private float m_totalIngoings;
   private float m_totalOutgoings;
   private float m_accountBalance;
   



    }
}