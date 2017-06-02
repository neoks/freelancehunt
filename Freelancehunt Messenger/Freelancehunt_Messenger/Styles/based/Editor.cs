using Xamarin.Forms;

namespace Freelancehunt_Messenger.Styles.based
{
    public class Editor : Xamarin.Forms.Editor
    {
        public Editor()
        {
        }

        #region ctrl + enter
        public static readonly BindableProperty ctrlEnterProperty = BindableProperty.Create(propertyName: "ctrlEnterProperty", returnType: typeof(void), declaringType: typeof(CtrlEnterEvent), defaultValue: typeof(CtrlEnterEvent));
        public delegate void CtrlEnterEvent();
        public event CtrlEnterEvent ctrlEnter;

        public void SetEvent_CtrlEnter()
        {
            ctrlEnter?.Invoke();
        }
        #endregion
    }
}
