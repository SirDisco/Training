using System;

namespace Support_Bank
{
    public class Transaction
    {
        public Transaction(ref Account sender, ref Account receiver, float amount, string narrative, DateTime date)
        {
            m_Sender = sender;
            m_Receiver = receiver;

            m_Amount = amount;
            m_Narrative = narrative;
            m_Date = date;
        }
        
        // Getters
        public Account GetSender() { return m_Sender; }
        public Account GetReceiver() { return m_Receiver; }
        
        public float GetAmount() { return m_Amount; }
        public string GetNarrative() { return m_Narrative; }
        public DateTime GetDate() { return m_Date; }

        // Members
        private Account m_Sender;
        private Account m_Receiver;

        private float m_Amount;
        private string m_Narrative;
        private DateTime m_Date;
    }
}