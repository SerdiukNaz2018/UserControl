using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Component
{
    public class Button: Control
    {
        private StringFormat SF = new StringFormat();
        private bool MouseEntered = false;
        private bool MousePressed = false;
        public Button()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;//позбавляємся від мерехтінь
            Size = new Size(100, 30);//Задаємо початковий розмір кнопки.
            BackColor = Color.BlueViolet;
            ForeColor = Color.White;//колір тексту
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;//вирівнюємо по виретикалі та горизонталі, щоб текст був всередині кнопки.
            
        }
        protected override void OnPaint(PaintEventArgs e) // Викликається, коли необхідно перемалювати об'єкт.
        {
            base.OnPaint(e);//Вкиликаємо базовий метод.
            Graphics graph = e.Graphics;// Об'являємо об'кт класу Graphics, з його допомогою виконується вся візуалізація графічного інтерфейсу.
            graph.SmoothingMode = SmoothingMode.HighQuality;//Вкахуємо високу якість візуалізації.
            graph.Clear(Parent.BackColor);//Викликаємо метод Clear, даний метод очищує всю поверхню малювання і заливає її вказаним кольором, в даному випадку BackColor батьківського контейнера
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);// Зменшили розмір прямокутника по ширині та висоті на один піксель,тому що малювалися б лише два ребра(ліве та верхнє), так сталолося тому, що інші два ребра проходять на границі видимій області кнопки.
            graph.DrawRectangle(new Pen(BackColor), rect);
            graph.FillRectangle(new SolidBrush(BackColor), rect);//заиваємо нашу кнопку.
            if (MouseEntered)//Якщо миша наведена на кнопку, то малюємо зверху ще один прямокутник поверх основного.
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rect);
            }
            if (MousePressed)//Якщо натискаємо на кнопку, то малюємо зверху ще один прямокутник поверх основного.
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
            }
            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseEntered = true;
            Invalidate();//Виклакаємо Invalidate, щоб метод OnPaint викликався кожен раз при наведені миші, таким чином буде виконуватись перемальовування кнопки з урахуванням змінених параметрів.
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseEntered = false;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MousePressed = true;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;
            Invalidate();
        }
    }
}
