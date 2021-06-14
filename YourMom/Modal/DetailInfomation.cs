using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class DetailInfomationClass : INotifyPropertyChanged
{
	protected string title;
	//0 là biểu đồ hình tròn
	//1 là biểu đồ hình cột
	protected bool typeOfChart;
	protected List<DetailCategory> components;

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

	public bool TypeOfChart
	{
		get
		{
			return typeOfChart;
		}
		set
		{
			typeOfChart = value;
			OnPropertyChanged("TypeOfChart");
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
