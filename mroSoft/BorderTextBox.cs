using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mroSoft
{
    class BorderTextBox : TextBox
    {
        private int borderRadius = 50; // Radio del borde redondeado
        private bool hasFocus = false; // Variable para rastrear si el control tiene el foco

        public BorderTextBox()
        {
            // Establecer el alto y el espaciado de línea
            this.Height = 36; // Puedes ajustar el alto según tus necesidades
            this.Font = new Font(this.Font.FontFamily, 14); // Tamaño de fuente y estilo

            // Cambiar el cursor cuando está deshabilitado
            this.Cursor = Cursors.IBeam;

            // Manejar el evento GotFocus y LostFocus para rastrear el enfoque
            this.GotFocus += (sender, e) => { hasFocus = true; this.Invalidate(); };
            this.LostFocus += (sender, e) => { hasFocus = false; this.Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Crear un rectángulo con bordes redondos
            int width = this.Width;
            int height = this.Height;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Esquina superior izquierda
                path.AddArc(width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Esquina superior derecha
                path.AddArc(width - borderRadius, height - borderRadius, borderRadius, borderRadius, 0, 90); // Esquina inferior derecha
                path.AddArc(0, height - borderRadius, borderRadius, borderRadius, 90, 90); // Esquina inferior izquierda
                path.CloseFigure();

                // Rellenar el fondo con el color deseado
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Dibujar el borde
                using (Pen pen = new Pen(hasFocus ? Color.Gray : this.ForeColor, 2))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(); // Redibujar el control cuando cambie el texto
        }
    }
}
