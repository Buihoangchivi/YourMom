using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class DetailInfomation : INotifyPropertyChanged
{
	protected string title;
	protected List<DetailCategory> components;
	protected double totalMoney;

	public string Title
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
			OnPropertyChanged("Title");
		}
	}

	public List<DetailCategory> Components
	{
		get
		{
			return components;
		}
		set
		{
			components = value;
			OnPropertyChanged("Components");
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
