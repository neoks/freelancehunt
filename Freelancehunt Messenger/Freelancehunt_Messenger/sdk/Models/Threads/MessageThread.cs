﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace Freelancehunt_Messenger.sdk.Models.Threads
{
    public class To
    {
        public string fname { get; set; }
        public string sname { get; set; }
    }

    public class Attachment
    {
        public string file_type { get; set; }
        public string url { get; set; }

        public string FileName
        {
            get
            {
                string fileName = Path.GetFileName(WebUtility.UrlDecode(url));
                return fileName.Length > 25 ? fileName.Remove(0, fileName.Length - 25) : fileName;
            }
        }
    }

    public class MessageThread
    {
        public From from { get; set; }
        public To to { get; set; }
        public string message_html { get; set; }
        public DateTime post_time { get; set; }
        public List<Attachment> attachments { get; set; }

        public string FullName
        {
            get
            {
                if (from.login != null && from.login.Contains("freelancehunt"))
                    return (Device.Idiom == TargetIdiom.Phone ? "@FH → " : "@Freelancehunt  →  ") + to.fname + " " + to.sname;

                return from.FullName;
            }
        }

        public bool IsMe
        {
            get
            {
                return from.profile_id == PlatformInvoke.profile_id;
            }
        }

        public bool IsPhoneAndIsMe
        {
            get
            {
                return Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone && IsMe;
            }
        }

        public bool IsAttachmentsNotNull
        {
            get
            {
                return (attachments != null && attachments.Count > 0);
            }
        }

        public bool IsFreelancehunt
        {
            get
            {
                if (from.login == null)
                    return false;

                return from.login.Contains("freelancehunt");
            }
        }
    }
}
