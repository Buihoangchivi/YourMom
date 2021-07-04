using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class TransactionList : Transaction
{

	//protected ObservableCollection<Transaction> transactions;

	//public ObservableCollection<Transaction> Transactions
	//{
	//	get
	//	{
	//		return transactions;
	//	}
	//	set
	//	{

	//		transactions = value;
	//		OnPropertyChanged("Transactions");
	//	}
	//}

	protected ObservableCollection<Transaction> transactions;
	protected int numberOfTransactions;
	protected double totalMoney;
	protected string imagePath;
	protected string transactionType;

	public ObservableCollection<Transaction> Transactions
    {
        get
        {
            return transactions;
        }
        set
        {

            transactions = value;
            OnPropertyChanged("Transactions");
        }
    }

	public int NumberOfTransactions
	{
		get
		{
			return numberOfTransactions;
		}
		set
		{
			numberOfTransactions = value;
			OnPropertyChanged("NumberOfTransactions");
		}
	}

	public double TotalMoney
	{
		get
		{
			return totalMoney;
		}
		set
		{
			totalMoney = value;
			OnPropertyChanged("TotalMoney");
		}
	}

	public string ImagePath
	{
		get
		{
			return imagePath;
		}
		set
		{
			imagePath = value;
			OnPropertyChanged("ImagePath");
		}
	}

	public string TransactionType
	{
		get
		{
			return transactionType;
		}
		set
		{
			transactionType = value;
			OnPropertyChanged("TransactionType");
		}
	}


}

