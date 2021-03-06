﻿namespace StockSharp.Xaml
{
	using System.Collections.Generic;
	using System.Linq;

	using Ecng.Serialization;
	using Ecng.Xaml;

	using StockSharp.Logging;

	/// <summary>
	/// Окно для мониторинга работы торговых стратегий.
	/// </summary>
	public partial class MonitorWindow : ILogListener
	{
		/// <summary>
		/// Создать <see cref="MonitorWindow"/>.
		/// </summary>
		public MonitorWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Выводить окно на передний экран в случае ошибки.
		/// </summary>
		public bool BringToFrontOnError { get; set; }

		/// <summary>
		/// Удалить все сообщения.
		/// </summary>
		public void Clear()
		{
			_monitor.Clear();
		}

		void ILogListener.WriteMessages(IEnumerable<LogMessage> messages)
		{
			((ILogListener)_monitor).WriteMessages(messages);

			if (BringToFrontOnError && messages.Any(message => message.Level == LogLevels.Error))
				this.BringToFront();
		}

		void IPersistable.Load(SettingsStorage storage)
		{
			_monitor.Load(storage);
			BringToFrontOnError = storage.GetValue<bool>("BringToFrontOnError");
		}

		void IPersistable.Save(SettingsStorage storage)
		{
			_monitor.Save(storage);
			storage.SetValue("BringToFrontOnError", BringToFrontOnError);
		}
	}
}