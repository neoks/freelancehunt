using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(Freelancehunt_Messenger.iOS.Renderer.ViewCellBase))]
namespace Freelancehunt_Messenger.iOS.Renderer
{
    public class ViewCellBase : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            // Достаем текущий ViewCell
            var cell = base.GetCell(item, reusableCell, tv);

            // Делаем копию и указываем новый BackgroundColor
            UIView view = new UIView(cell.SelectedBackgroundView.Bounds);
            view.Layer.BackgroundColor = UIColor.FromRGB(220, 220, 225).CGColor;
            cell.SelectedBackgroundView = view;

            // ОТдаем результат
            return cell;
        }
    }
}
