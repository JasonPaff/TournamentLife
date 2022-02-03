using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Tournament_Life.Models.Bankroll_Model;

namespace Tournament_Life.Helpers
{
    public static class BankrollHelper
    {
        public static List<BankrollListBoxItem> CreateListBoxItems(List<string> venues)
        {
            var items = new List<BankrollListBoxItem>();

            foreach (var venue in venues)
            {
                var item = new BankrollListBoxItem
                {
                    IsSelected = false,
                    Name = venue,
                };
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Creates a list of list box items from a list of bankrolls
        /// </summary>
        /// <param name="bankrolls">list of bankrolls</param>
        /// <returns>list box items collection</returns>
        public static List<BankrollListBoxItem> CreateListBoxItems(List<Bankroll> bankrolls)
        {
            var items = new List<BankrollListBoxItem>();

            foreach (var bankroll in bankrolls)
            {
                var item = new BankrollListBoxItem
                {
                    IsSelected = false,
                    Name = bankroll.Venue,
                };
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// delete a bankroll from the bankrolls xml file
        /// </summary>
        /// <param name="bankroll">bankroll to delete</param>
        public static void DeleteBankroll(Bankroll bankroll)
        {
            // null check
            if (bankroll is null) 
                return;

            // bankrolls filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.BankrollFileName;

            // load bankrolls xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // get bankroll nodes
            var bankrollNodes = doc.DocumentElement?.ChildNodes;
            if (bankrollNodes is null || bankrollNodes.Count is 0) 
                return;

            // find the bankroll we want
            foreach (XmlNode bankrollNode in bankrollNodes)
            {
                // not our bankroll, go next
                if (bankrollNode.SelectSingleNode(nameof(Transaction.Venue))?.InnerText != bankroll.Venue)
                    continue;

                // remove bankroll
                doc.DocumentElement?.RemoveChild(bankrollNode);

                // save
                doc.Save(filename);

                // leave
                return;
            }
        }

        /// <summary>
        /// delete bankrolls from the bankrolls xml file
        /// </summary>
        /// <param name="bankrolls">bankrolls to remove</param>
        public static void DeleteBankrolls(List<Bankroll> bankrolls)
        {
            // null/zero check
            if (bankrolls is null || bankrolls.Count is 0)
                return;

            // delete bankrolls
            foreach(var bankroll in bankrolls)
                DeleteBankroll(bankroll);
        }

        /// <summary>
            /// Delete transactions from a bankroll in the bankrolls xml file
            /// </summary>
            /// <param name="bankroll">bankroll to edit</param>
            /// <param name="transactions">transactions to remove</param>
        public static void DeleteTransactions(Bankroll bankroll, List<Transaction> transactions)
        {
            // null/zero check
            if (bankroll is null || transactions is null || transactions.Count is 0)
                return;

            // bankrolls filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.BankrollFileName;

            // load bankrolls xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // get bankroll nodes
            var bankrollNodes = doc.DocumentElement?.ChildNodes;

            // null/zero check
            if (bankrollNodes is null || bankrollNodes.Count is 0) 
                return;

            // nodes to remove
            var nodesToRemove = new List<XmlNode>();

            // find the bankroll we want
            foreach (XmlNode bankrollNode in bankrollNodes)
            {
                // not our bankroll, go next
                if (bankrollNode.SelectSingleNode(nameof(Transaction.Venue))?.InnerText != bankroll.Venue) 
                    continue;

                // get transaction nodes
                var transactionNodes = bankrollNode.SelectSingleNode("Transactions");

                // no transactions, go next
                if (transactionNodes is null || !transactionNodes.HasChildNodes) 
                    continue;

                // loop through transaction node
                foreach (XmlNode transactionNode in transactionNodes)
                {
                    // find matching transaction
                    var transaction = transactions.FirstOrDefault(i => i.Amount - decimal.Parse(transactionNode.SelectSingleNode(nameof(Transaction.Amount))?.InnerText ?? "0") is 0 &&
                                                                       i.Venue == transactionNode.SelectSingleNode(nameof(Transaction.Venue))?.InnerText &&
                                                                       i.Date == DateTime.Parse(transactionNode.SelectSingleNode(nameof(Transaction.Date))?.InnerText ?? "1/1/1111 12:00:00 AM") &&
                                                                       (int)i.TransactionType == int.Parse(transactionNode.SelectSingleNode(nameof(Transaction.TransactionType))?.InnerText ?? "0"));

                    // match found, add to remove transaction list
                    if (transaction is not null) nodesToRemove.Add(transactionNode);
                }

                // loop transaction to remove and remove them
                foreach (var node in nodesToRemove) 
                    if (node is not null)
                        bankrollNode.SelectSingleNode("Transactions").RemoveChild(node);
            }

            // save file
            doc.Save(filename);
        }

        /// <summary>
        /// loads the bankrolls from the xml file
        /// </summary>
        /// <returns>bankrolls collection</returns>
        public static List<Bankroll> LoadBankrolls()
        {
            // bankrolls collection
            var bankrolls = new List<Bankroll>();

            // bankrolls filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.BankrollFileName;

            // load bankrolls xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // create default bankrolls file if none exists
            if (doc is null)
            {
                doc = new XmlDocument();
                var rootNode = doc.CreateElement("Bankrolls");
                doc.AppendChild(rootNode);
                doc.Save(filename);
                doc = XmlHelper.LoadXmlFile(filename);
            }

            // get list of bankroll nodes
            var bankrollNodes = doc.DocumentElement?.SelectNodes(nameof(Bankroll));

            // no list, leave
            if (bankrollNodes is null) return bankrolls;

            // loop bankrolls and add them to bankroll collection
            foreach (XmlNode bankrollNode in bankrollNodes)
            {
                // create new bankroll
                var bankroll = new Bankroll()
                {
                    Venue = bankrollNode.SelectSingleNode(nameof(Bankroll.Venue))?.InnerText,
                    StartingAmount = double.Parse(bankrollNode.SelectSingleNode(nameof(Bankroll.StartingAmount))?.InnerText ?? "0"),
                };

                // add bankroll to bankrolls
                bankrolls.Add(bankroll);

                // load transactions
                var transactionNodes = bankrollNode.SelectSingleNode("Transactions");
                if (transactionNodes is null) continue;

                // loop transactions
                foreach (XmlNode transactionNode in transactionNodes)
                {
                    // create transaction
                    var transaction = new Transaction()
                    {
                        Amount = decimal.Parse(transactionNode.SelectSingleNode(nameof(Transaction.Amount))?.InnerText ?? "0"),
                        Date = DateTime.Parse(transactionNode.SelectSingleNode(nameof(Transaction.Date))?.InnerText ?? DateTime.Now.ToString(new CultureInfo("en-US"))),
                        TransactionType = (TransactionTypes)int.Parse(transactionNode?.SelectSingleNode(nameof(Transaction.TransactionType))?.InnerText ?? "0"),
                        Venue = transactionNode.SelectSingleNode(nameof(Transaction.Venue))?.InnerText ?? "",
                    };

                    // add bonus type if transaction type is bonus
                    if (transaction.TransactionType == TransactionTypes.Bonus) transaction.BonusType = (BonusTypes)int.Parse(transactionNode?.SelectSingleNode(nameof(Transaction.BonusType))?.InnerText ?? "0");

                    // add transaction to bankroll
                    bankroll.AddTransaction(transaction);
                }
            }

            // return
            return bankrolls;
        }

        /// <summary>
        /// Saves the bankroll to the xml file
        /// </summary>
        /// <param name="bankroll">bankroll to add/save</param>
        public static void SaveBankroll(Bankroll bankroll)
        {
            // null check
            if (bankroll is null) return;

            // bankrolls filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.BankrollFileName;

            // load bankrolls from file
            var bankrolls = LoadBankrolls();

            // add new bankroll to collection
            bankrolls.Add(bankroll);

            // load bankrolls xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // bankroll node
            var bankrollNode = doc.CreateElement(nameof(Bankroll));

            // venue
            var node = doc.CreateElement(nameof(bankroll.Venue));
            node.InnerText = bankroll.Venue;
            bankrollNode.AppendChild(node);

            // starting amount
            node = doc.CreateElement(nameof(bankroll.StartingAmount));
            node.InnerText = bankroll.StartingAmount.ToString(new CultureInfo("en-US"));
            bankrollNode.AppendChild(node);

            // transactions node
            var transactionsNode = doc.CreateElement(nameof(bankroll.Transactions));

            // loop through the transactions
            foreach (var transaction in bankroll.Transactions)
            {
                // transaction node
                XmlNode transactionNode = doc.CreateElement(nameof(Transaction));

                // amount
                XmlNode transactionChildNode = doc.CreateElement(nameof(transaction.Amount));
                transactionChildNode.InnerText = transaction.Amount.ToString(new CultureInfo("en-US"));
                transactionNode.AppendChild(transactionChildNode);

                // date
                transactionChildNode = doc.CreateElement(nameof(transaction.Date));
                transactionChildNode.InnerText = transaction.Amount.ToString(new CultureInfo("en-US"));
                transactionNode.AppendChild(transactionChildNode);

                // transaction type
                transactionChildNode = doc.CreateElement(nameof(transaction.TransactionType));
                transactionChildNode.InnerText = ((int)transaction.TransactionType).ToString();
                transactionNode.AppendChild(transactionChildNode);

                // venue
                transactionChildNode = doc.CreateElement(nameof(transaction.Venue));
                transactionChildNode.InnerText = transaction.Amount.ToString(new CultureInfo("en-US"));
                transactionNode.AppendChild(transactionChildNode);

                // add node to transactions node
                transactionsNode.AppendChild(transactionNode);
            }

            // add transactions node to bankroll node
            bankrollNode.AppendChild(transactionsNode);

            // add bankroll node to root node
            doc.DocumentElement?.AppendChild(bankrollNode);

            // save
            doc.Save(filename);
        }

        /// <summary>
        /// Saves a transaction to the bankroll
        /// </summary>
        /// <param name="bankroll">bankroll</param>
        /// <param name="transaction">transaction</param>
        public static void SaveTransaction(Bankroll bankroll, Transaction transaction)
        {
            // null check
            if (bankroll is null || transaction is null) return;

            // bankrolls filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.BankrollFileName;

            // load bankrolls xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // get bankroll nodes
            var bankrollNodes = doc.DocumentElement?.ChildNodes;
            if (bankrollNodes is null || bankrollNodes.Count is 0) return;

            // loop bankroll nodes and add transaction to correct bankrolls transactions
            foreach (XmlNode bankrollNode in bankrollNodes)
            {
                // go next cycle if not the right bankroll node
                if (bankrollNode.SelectSingleNode("Venue")?.InnerText != bankroll.Venue) continue;

                // new transaction node
                XmlNode transactionRootNode = doc.CreateElement(nameof(Transaction));

                // amount
                XmlNode transactionChildNode = doc.CreateElement(nameof(Transaction.Amount));
                transactionChildNode.InnerText = transaction.Amount.ToString(new CultureInfo("en-US"));
                transactionRootNode.AppendChild(transactionChildNode);

                // date
                transactionChildNode = doc.CreateElement(nameof(Transaction.Date));
                transactionChildNode.InnerText = transaction.Date.ToString(new CultureInfo("en-US"));
                transactionRootNode.AppendChild(transactionChildNode);

                // type
                transactionChildNode = doc.CreateElement(nameof(Transaction.TransactionType));
                transactionChildNode.InnerText = ((int)transaction.TransactionType).ToString();
                transactionRootNode.AppendChild(transactionChildNode);

                // venue
                transactionChildNode = doc.CreateElement(nameof(Transaction.Venue));
                transactionChildNode.InnerText = transaction.Venue;
                transactionRootNode.AppendChild(transactionChildNode);

                // maybe bonus type
                if (transaction.TransactionType == TransactionTypes.Bonus)
                {
                    transactionChildNode = doc.CreateElement(nameof(Transaction.BonusType));
                    transactionChildNode.InnerText = ((int)transaction.BonusType).ToString();
                    transactionRootNode.AppendChild(transactionChildNode);
                }

                // add new transaction node to transactions nodes
                bankrollNode.SelectSingleNode("Transactions")?.AppendChild(transactionRootNode);
            }

            // save
            doc.Save(filename);
        }
    }
}
