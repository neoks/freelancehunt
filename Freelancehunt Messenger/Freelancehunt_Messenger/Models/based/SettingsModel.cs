using System;

namespace Freelancehunt_Messenger.Models.based
{
    public class SettingsModel
    {
        /// <summary>
        /// Диалоги на главной
        /// </summary>
        public short CountMsgToPage { get; set; }

        /// <summary>
        /// Сообщений внутри диалога
        /// </summary>
        public short CountMsgToDialog { get; set; }
    }
}
