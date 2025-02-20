﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class TransactionList : INotifyPropertyChanged
{

	protected ObservableCollection<DetailTransaction> transactions;
	protected DateTime date;
	protected double totalMoney;

	public ObservableCollection<DetailTransaction> Transactions
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

	public DateTime Date
	{
		get
		{
			return date;
		}
		set
		{
			date = value;
			OnPropertyChanged("Date");
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

	#region INotifyPropertyChanged Members  

	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged(string propertyName)
	{
		if (PropertyChanged != null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	#endregion

}

