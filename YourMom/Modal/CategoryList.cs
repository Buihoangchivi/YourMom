using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class CategoryList: INotifyPropertyChanged
{

	protected ObservableCollection<Transaction> transactions;
	protected int numberOfTransactions;
	protected double totalMoney;
	protected string imagePath;
	protected string name;

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

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			OnPropertyChanged("Name");
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

