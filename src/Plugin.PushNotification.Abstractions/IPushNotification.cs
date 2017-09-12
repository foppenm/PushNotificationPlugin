﻿using System;
using System.Collections.Generic;

namespace Plugin.PushNotification.Abstractions
{
    public enum NotificationActionType
    {
        Default,
        AuthenticationRequired, //Only applies for iOS
        Foreground,
        Destructive  //Only applies for iOS
    }
    public class NotificationUserCategory
    {
        public string Category { get; }
        public List<NotificationUserAction> Actions { get; }

        public NotificationCategoryType Type { get; }

        public NotificationUserCategory(string category, List<NotificationUserAction> actions, NotificationCategoryType type = NotificationCategoryType.Default)
        {
            Category = category;
            Actions = actions;
            Type = type;
        }
    }

    public class NotificationUserAction
    {
        public string Id { get; }
        public string Title { get; }
        public NotificationActionType Type { get; }
        public string Icon { get; }
        public NotificationUserAction(string id, string title, NotificationActionType type = NotificationActionType.Default, string icon = "")
        {
            Id = id;
            Title = title;
            Type = type;
            Icon = icon;
        }
    }

    public enum NotificationCategoryType
    {
        Default,
        Custom,
        Dismiss
    }

    public delegate void PushNotificationTokenEventHandler(object source, PushNotificationTokenEventArgs e);

    public class PushNotificationTokenEventArgs : EventArgs
    {
        public string Token { get; }

        public PushNotificationTokenEventArgs(string token)
        {
            Token = token;
        }

    }

    public delegate void PushNotificationErrorEventHandler(object source, PushNotificationErrorEventArgs e);

    public class PushNotificationErrorEventArgs : EventArgs
    {
        public string Message { get; }

        public PushNotificationErrorEventArgs(string message)
        {
            Message = message;
        }

    }

    public delegate void PushNotificationDataEventHandler(object source, PushNotificationDataEventArgs e);

    public class PushNotificationDataEventArgs : EventArgs
    {
        public IDictionary<string, string> Data { get; }

        public PushNotificationDataEventArgs(IDictionary<string, string> data)
        {
            Data = data;
        }

    }


    public delegate void PushNotificationResponseEventHandler(object source, PushNotificationResponseEventArgs e);

    public class PushNotificationResponseEventArgs : EventArgs
    {
        public string Identifier { get; }

        public IDictionary<string, string> Data { get; }

        public NotificationCategoryType Type { get; }

        public PushNotificationResponseEventArgs(IDictionary<string, string> data, string identifier = "", NotificationCategoryType type = NotificationCategoryType.Default)
        {
            Identifier = identifier;
            Data = data;
            Type = type;
        }

    }

    /// <summary>
    /// Interface for PushNotification
    /// </summary>
  public interface IPushNotification
  {
        /// <summary>
        /// Notification handler to receive, customize notification feedback and provide user actions
        /// </summary>
        IPushNotificationHandler NotificationHandler { get; set; }
        /// <summary>
        /// Event triggered when token is refreshed
        /// </summary>
        event PushNotificationTokenEventHandler OnTokenRefresh;
        /// <summary>
        /// Event triggered when a notification is opened
        /// </summary>
        event PushNotificationResponseEventHandler OnNotificationOpened;
        /// <summary>
        /// Event triggered when a notification is received
        /// </summary>
        event PushNotificationDataEventHandler OnNotificationReceived;
        /// <summary>
        /// Event triggered when there's an error
        /// </summary>
        event PushNotificationErrorEventHandler OnNotificationError;
        /// <summary>
        /// Push notification token
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Get all user notification categories
        /// </summary>
        NotificationUserCategory[] GetUserNotificationCategories();
    }
}