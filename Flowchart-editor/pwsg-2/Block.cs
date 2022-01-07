using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
using System.Runtime.Serialization;

namespace pwsg_2
{
    [Serializable]
    abstract class Block : ISerializable
    {
        public string text;
        protected Font drawFont;
        protected SolidBrush drawBrush;
        protected StringFormat drawFormat;
        public bool editable { get; protected set; }
        public Graphics flagGraphics { get; protected set; }
        public Pen drawPen;     
        public PictureBox picture { get; protected set; }
        public int x { get; protected set; }
        public int y {  get; protected set; }

        //добавили в параметры высоту и ширину фигуры
        public int height;
        public int weidth;
        public Block()
        {
            drawFont = new Font("Microsoft Sans Serif", 8);
            drawBrush = new SolidBrush(Color.Black);
            drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawPen = new Pen(Color.Black, 2);
        }
        public Block(PictureBox p, Graphics g) 
        {
            drawFont = new Font("Microsoft Sans Serif", 8);
            drawBrush = new SolidBrush(Color.Black);
            drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawPen = new Pen(Color.Black, 2);
            flagGraphics = g;
            picture = p;
            editable = true;
        }
        public void SetGraphics(PictureBox p,Graphics g) { picture = p; flagGraphics = g; }
        public void SetCoordinates(int x, int y) { this.x = x; this.y = y; }
        protected void PrintPoints()
        {
         
        }
        public virtual void Clear()
        {
            if(drawFont!=null) drawFont.Dispose();
            if (drawBrush != null) drawBrush.Dispose();
            if (drawFormat != null) drawFormat.Dispose();
            if (drawPen != null) drawPen.Dispose();
            flagGraphics = null;
            picture = null;
        }
        public abstract void Print(bool refresh=true);
        public abstract void BaseText();
        public abstract Block CloneNew();
        public abstract bool IsInside(int x, int y);

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            
            info.AddValue("x", x);
            info.AddValue("y", y);
            info.AddValue("text", text);
            info.AddValue("text", text);

            info.AddValue("height", height);
            info.AddValue("weidth", weidth);



        }
        public Block(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            height = (int)info.GetValue("height", typeof(int));
            weidth = (int)info.GetValue("weidth", typeof(int));

            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));        
        }
    }
    [Serializable]
    class OperationBlock:Block, ISerializable
    {
        public OperationBlock():base() { }
        public OperationBlock(PictureBox p,Graphics g) : base(p,g)
        {
           
        }
        public override void Print(bool refresh=true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 50, y - 25), new Size(100, 50));
            flagGraphics.FillRectangle(Brushes.White, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();

            
            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ДЕЙСТВИЕ";
            height = 50;
            weidth = 100;
        }
        public override Block CloneNew()
        {
            OperationBlock other = new OperationBlock(this.picture,this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 25) return false;
            if (y <= this.y - 25) return false;
            return true;
        }
        public OperationBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }
    [Serializable]
    class Arrow : Block, ISerializable
    {
        public Arrow() : base() { }
        public Arrow(PictureBox p, Graphics g) : base(p, g)
        {}
        public override void Print(bool refresh = true)
        {
 
            Rectangle displayRectangle = new Rectangle(new Point(x - 50, y - 25), new Size(3, 53));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "СТРЕЛКА";
            height = 53;
            weidth = 3;
        }
        public override Block CloneNew()
        {
            Arrow other = new Arrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public Arrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class LongVerticalArrow : Block, ISerializable
    {
        public LongVerticalArrow() : base() { }
        public LongVerticalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 50, y - 25), new Size(3, 100));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ДЛИННАЯ_ВЕРТИКАЛЬНАЯ_СТРЕКЛКА";
            height = 53;
            weidth = 3;
        }
        public override Block CloneNew()
        {
            LongVerticalArrow other = new LongVerticalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public LongVerticalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class HorizantalArrow : Block, ISerializable
    {
        public HorizantalArrow() : base() { }
        public HorizantalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(52, 3));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            HorizantalArrow other = new HorizantalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public HorizantalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class MiddleHorizantalArrow : Block, ISerializable
    {
        public MiddleHorizantalArrow() : base() { }
        public MiddleHorizantalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(100, 3));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            MiddleHorizantalArrow other = new MiddleHorizantalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public MiddleHorizantalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class LongMiddleHorizantalArrow : Block, ISerializable
    {
        public LongMiddleHorizantalArrow() : base() { }
        public LongMiddleHorizantalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(103, 3));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            LongMiddleHorizantalArrow other = new LongMiddleHorizantalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public LongMiddleHorizantalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }


    [Serializable]
    class MiddleVerticalArrow : Block, ISerializable
    {
        public MiddleVerticalArrow() : base() { }
        public MiddleVerticalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(3, 220));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            MiddleVerticalArrow other = new MiddleVerticalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public MiddleVerticalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class VeryLongVerticalArrow : Block, ISerializable
    {
        public VeryLongVerticalArrow() : base() { }
        public VeryLongVerticalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(3, 300));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            VeryLongVerticalArrow other = new VeryLongVerticalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public VeryLongVerticalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }

    [Serializable]
    class LongHorizantalArrow : Block, ISerializable
    {
        public LongHorizantalArrow() : base() { }
        public LongHorizantalArrow(PictureBox p, Graphics g) : base(p, g)
        { }
        public override void Print(bool refresh = true)
        {

            Rectangle displayRectangle = new Rectangle(new Point(x - 25, y - 50), new Size(302, 3));
            flagGraphics.FillRectangle(Brushes.Black, displayRectangle);
            flagGraphics.DrawRectangle(drawPen, displayRectangle);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ДЛИННАЯ_ГОРИЗОНТАЛЬНАЯ_СТРЕЛКА";
            height = 3;
            weidth = 53;
        }
        public override Block CloneNew()
        {
            LongHorizantalArrow other = new LongHorizantalArrow(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 20) return false;
            if (y <= this.y - 20) return false;
            return true;
        }
        public LongHorizantalArrow(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }
    [Serializable]
    class DecisionBlock : Block, ISerializable
    {
        public DecisionBlock() { }
        public DecisionBlock(PictureBox p,Graphics g) : base(p,g)
        {
        }
        public override void BaseText()
        {
            text = "ЛОГИЧЕСКОЕ УСЛОВИЕ";
            height = 46;
            weidth = 80;
        }
        public override void Print(bool refresh = true)
        {
            Point []rhomb= new Point[4] { new Point(x - 100, y), new Point(x, y - 50), new Point(x + 100, y), new Point(x, y + 50) };
            Rectangle displayRectangle = new Rectangle(new Point(x - 40, y - 18), new Size(80,46));
            flagGraphics.FillPolygon(Brushes.White, rhomb);
            flagGraphics.DrawPolygon(drawPen,rhomb);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            flagGraphics.DrawString("True", drawFont, drawBrush, x - 100,y - 15, drawFormat);
            flagGraphics.DrawString("False", drawFont, drawBrush, x + 100, y - 15, drawFormat);
            PrintPoints();

            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override Block CloneNew()
        {         
            DecisionBlock other = new DecisionBlock(this.picture,this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (((x - this.x) *(x - this.x) / (50.0 * 50.0) + (y - this.y) * (y - this.y) / (37.0 * 37.0))<1)
                return true;
            else return false;
        }
        public DecisionBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));   
        }
    }
    [Serializable]
    class InputBlock : Block, ISerializable
    {
        public InputBlock() : base() { }
        public InputBlock(PictureBox p, Graphics g) : base(p, g)
        {
        }
        public override void Print(bool refresh = true)
        {
            Point[] oblique = new Point[4] { new Point(x - 50, y + 20), new Point(x - 35, y - 20), new Point(x + 50, y - 20), new Point(x + 35, y + 20) };
            Rectangle displayRectangle = new Rectangle(new Point(x - 50, y - 25), new Size(100, 50));
            flagGraphics.FillPolygon(Brushes.White, oblique);
            flagGraphics.DrawPolygon(drawPen, oblique);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ВВОД/ВЫВОД";
            height = 50;
            weidth = 100;
        }
        public override Block CloneNew()
        {
            InputBlock other = new InputBlock(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 25) return false;
            if (y <= this.y - 25) return false;
            return true;
        }
        public InputBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));         
        }
    }
    [Serializable]
    class CycleBlock : Block, ISerializable
    {
        public CycleBlock() : base() { }
        public CycleBlock(PictureBox p, Graphics g) : base(p, g)
        {
        }
        public override void Print(bool refresh = true)
        {
            Point[] hexagon = new Point[6] { new Point(x - 60, y), new Point(x - 40, y - 20), new Point(x + 40, y - 20), new Point(x + 60, y),
                                                                                                        new Point(x + 40, y + 20), new Point(x - 40, y + 20)};
            Rectangle displayRectangle = new Rectangle(new Point(x - 50, y - 25), new Size(100, 50));
            flagGraphics.FillPolygon(Brushes.White, hexagon);
            flagGraphics.DrawPolygon(drawPen, hexagon);
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();


            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override void BaseText()
        {
            text = "ЦИКЛ";
            height = 50;
            weidth = 100;
        }
        public override Block CloneNew()
        {
            CycleBlock other = new CycleBlock(this.picture, this.flagGraphics);
            other.BaseText();
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (x >= this.x + 50) return false;
            if (x <= this.x - 50) return false;
            if (y >= this.y + 25) return false;
            if (y <= this.y - 25) return false;
            return true;
        }
        public CycleBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }
    [Serializable]
    class StartBlock : Block, ISerializable
    {
        static bool exists=false;
        public StartBlock():base()
        {
            drawPen = new Pen(Color.Black, 2);
            editable = false;
        }
        public StartBlock(PictureBox p, Graphics g) : base(p, g)
        {
            drawPen = new Pen(Color.Black, 2);
            editable = false;
        }
        public override void BaseText()
        {

            text = "НАЧАЛО";
        }
        public override void Print(bool refresh = true)
        {
            Rectangle displayRectangle = new Rectangle(new Point(x - 24, y - 18), new Size(53, 36));
            flagGraphics.FillEllipse(Brushes.White, new Rectangle(x - 40, y - 25, 80, 50));
            flagGraphics.DrawEllipse(drawPen, new Rectangle(x - 40, y - 25, 80, 50));
            flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
            PrintPoints();

            if (refresh)
            {
                flagGraphics.DrawImage(picture.Image, 0, 0);
                picture.Refresh();
            }
        }
        public override Block CloneNew()
        {
            StartBlock other = new StartBlock(this.picture, this.flagGraphics);
            other.BaseText();
            exists = true;
            return other;
        }
        public override bool IsInside(int x, int y)
        {
            if (((x - this.x) * (x - this.x) / (50.0 * 50.0) + (y - this.y) * (y - this.y) / (37.0 * 37.0)) < 1)
                return true;
            else return false;
        }
        public override void Clear()
        {
            base.Clear();
            exists = false;
        }
        public new void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {

            info.AddValue("x", x);
            info.AddValue("y", y);
            info.AddValue("text", text);
            info.AddValue("editable", editable);
            info.AddValue("exists", exists);
    

        }
        public StartBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
            exists = (bool)info.GetValue("exists", typeof(bool));

        }
    }
    [Serializable]
    class StopBlock : Block, ISerializable
    {
            public StopBlock() { }
            public StopBlock(PictureBox p, Graphics g) : base(p, g)
            {
                drawPen = new Pen(Color.Black, 2);
                editable = false;
            }
            public override void BaseText()
            {

                text = "КОНЕЦ";
            }
            public override void Print(bool refresh = true)
            {
                Rectangle displayRectangle = new Rectangle(new Point(x - 24, y - 18), new Size(48, 36));
                flagGraphics.FillEllipse(Brushes.White, new Rectangle(x - 40, y - 25, 80, 50));
                flagGraphics.DrawEllipse(drawPen, new Rectangle(x - 40, y - 25, 80, 50));
                flagGraphics.DrawString(text, drawFont, drawBrush, displayRectangle, drawFormat);
                PrintPoints();

                if (refresh)
                {
                    flagGraphics.DrawImage(picture.Image, 0, 0);
                    picture.Refresh();
                }
            }
            public override Block CloneNew()
            {
                StopBlock other = new StopBlock(this.picture, this.flagGraphics);
                other.BaseText();
                return other;
            }
            public override bool IsInside(int x, int y)
            {
                if (((x - this.x) * (x - this.x) / (50.0 * 50.0) + (y - this.y) * (y - this.y) / (37.0 * 37.0)) < 1)
                    return true;
                else return false;
            }
        public StopBlock(SerializationInfo info, StreamingContext ctxt)
        {
            x = (int)info.GetValue("x", typeof(int));
            y = (int)info.GetValue("y", typeof(int));
            text = (string)info.GetValue("text", typeof(string));
            editable = (bool)info.GetValue("editable", typeof(bool));
        }
    }
}
