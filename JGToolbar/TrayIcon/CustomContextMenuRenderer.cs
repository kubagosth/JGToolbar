using System.Drawing;
using System.Windows.Forms;

namespace JGToolbar.TrayIcon
{
    public class CustomContextMenuRenderer : ToolStripProfessionalRenderer
    {
        public CustomContextMenuRenderer() : base(new CustomProfessionalColors()) { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(51, 55, 60), 1))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var rect = new Rectangle(Point.Empty, e.Item.Size);

            if (e.Item.Selected)
            {
                using (var brush = new SolidBrush(Color.FromArgb(85, 91, 101)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                using (var brush = new SolidBrush(Color.FromArgb(51, 55, 60)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(85, 91, 101)))
            {
                var y = e.Item.Bounds.Height / 2;
                e.Graphics.DrawLine(pen, e.Item.Bounds.Left + 10, y, e.Item.Bounds.Right - 10, y);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.White;
            e.TextFont = new Font("Segoe UI", 9, FontStyle.Bold);
            base.OnRenderItemText(e);
        }
    }

    public class CustomProfessionalColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(85, 91, 101);
        public override Color MenuItemBorder => Color.FromArgb(51, 55, 60);
        public override Color ToolStripDropDownBackground => Color.FromArgb(51, 55, 60);
        public override Color ImageMarginGradientBegin => Color.FromArgb(51, 55, 60);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(51, 55, 60);
        public override Color ImageMarginGradientEnd => Color.FromArgb(51, 55, 60);
    }
}
