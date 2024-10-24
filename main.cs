using System;
using System.Drawing;
using System.Windows.Forms;

public class Form1 : Form
{
    private Point line1Start = new Point(50, 50);
    private Point line1End = new Point(150, 50);
    private Point line2Start = new Point(150, 50);
    private Point line2End = new Point(150, 150);
    private Point line3Start = new Point(150, 150);
    private Point line3End = new Point(50, 150);
    private Point line4Start = new Point(50, 150);
    private Point line4End = new Point(50, 50);
    private Point trianglePoint1 = new Point(200, 200);
    private Point trianglePoint2 = new Point(250, 150);
    private Point trianglePoint3 = new Point(150, 200);
    
    private bool isMovable = true;

    public Form1()
    {
        this.Text = "Movable Rectangle and Triangle";
        this.Size = new Size(400, 400);
        this.FormBorderStyle = FormBorderStyle.None;
        this.MouseDown += Form1_MouseDown;
        this.MouseMove += Form1_MouseMove;
        this.Paint += Form1_Paint;
        this.KeyDown += Form1_KeyDown;
        this.KeyPreview = true; // Permitir que el formulario reciba teclas antes de los controles
    }

    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && isMovable)
        {
            // Comprobar si el clic está cerca de alguna línea
            if (IsPointOnLine(e.Location, line1Start, line1End) ||
                IsPointOnLine(e.Location, line2Start, line2End) ||
                IsPointOnLine(e.Location, line3Start, line3End) ||
                IsPointOnLine(e.Location, line4Start, line4End) ||
                IsPointOnLine(e.Location, trianglePoint1, trianglePoint2) ||
                IsPointOnLine(e.Location, trianglePoint2, trianglePoint3) ||
                IsPointOnLine(e.Location, trianglePoint3, trianglePoint1))
            {
                this.Capture = true;
                this.MouseMove += Form1_MouseMove; // Mover la forma
            }
        }
    }

    private void Form1_MouseMove(object sender, MouseEventArgs e)
    {
        if (this.Capture && isMovable)
        {
            // Mover el rectángulo
            line1Start.Offset(e.X - line1Start.X, e.Y - line1Start.Y);
            line1End.Offset(e.X - line1End.X, e.Y - line1End.Y);
            line2Start.Offset(e.X - line2Start.X, e.Y - line2Start.Y);
            line2End.Offset(e.X - line2End.X, e.Y - line2End.Y);
            line3Start.Offset(e.X - line3Start.X, e.Y - line3Start.Y);
            line3End.Offset(e.X - line3End.X, e.Y - line3End.Y);
            line4Start.Offset(e.X - line4Start.X, e.Y - line4Start.Y);
            line4End.Offset(e.X - line4End.X, e.Y - line4End.Y);
            trianglePoint1.Offset(e.X - trianglePoint1.X, e.Y - trianglePoint1.Y);
            trianglePoint2.Offset(e.X - trianglePoint2.X, e.Y - trianglePoint2.Y);
            trianglePoint3.Offset(e.X - trianglePoint3.X, e.Y - trianglePoint3.Y);
            this.Invalidate(); // Redibujar
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        this.Capture = false;
        base.OnMouseUp(e);
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.DrawLine(Pens.Black, line1Start, line1End);
        g.DrawLine(Pens.Black, line2Start, line2End);
        g.DrawLine(Pens.Black, line3Start, line3End);
        g.DrawLine(Pens.Black, line4Start, line4End);
        // Dibujar el triángulo
        g.DrawLine(Pens.Red, trianglePoint1, trianglePoint2);
        g.DrawLine(Pens.Red, trianglePoint2, trianglePoint3);
        g.DrawLine(Pens.Red, trianglePoint3, trianglePoint1);
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.G)
        {
            isMovable = !isMovable; // Alternar el estado de movimiento
        }
    }

    private bool IsPointOnLine(Point p, Point start, Point end)
    {
        const int threshold = 5;
        return Math.Abs((end.Y - start.Y) * p.X - (end.X - start.X) * p.Y + end.X * start.Y - end.Y * start.X) < threshold;
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new Form1());
    }
}
