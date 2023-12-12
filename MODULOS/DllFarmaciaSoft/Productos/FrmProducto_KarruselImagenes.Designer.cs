namespace DllFarmaciaSoft.Productos
{
    partial class FrmProducto_KarruselImagenes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProducto_KarruselImagenes));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnCargarImagenes = new System.Windows.Forms.ToolStripButton();
            this.separadorCargarImagenes = new System.Windows.Forms.ToolStripSeparator();
            this.btnImg_Atras = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImg_Adelante = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMostrarTodasLasImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotarIzquierda = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotarDerecha = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblMensaje = new System.Windows.Forms.ToolStripLabel();
            this.lblNombreImagen = new SC_ControlsCS.scLabelExt();
            this.lblNdeM = new System.Windows.Forms.Label();
            this.picImagen = new System.Windows.Forms.PictureBox();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtClaveInternaSal = new System.Windows.Forms.Label();
            this.txtContenido = new SC_ControlsCS.scLabelExt();
            this.lblPresentacion = new SC_ControlsCS.scLabelExt();
            this.lblSegmento = new SC_ControlsCS.scLabelExt();
            this.lblSubFamilia = new SC_ControlsCS.scLabelExt();
            this.lblFamilia = new SC_ControlsCS.scLabelExt();
            this.lblClasificacionSSA = new SC_ControlsCS.scLabelExt();
            this.txtDescripcionCorta = new SC_ControlsCS.scLabelExt();
            this.txtDescripcion = new SC_ControlsCS.scLabelExt();
            this.lblTipoDeProducto = new SC_ControlsCS.scLabelExt();
            this.chkEsSectorSalud = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblDescripcionSal = new System.Windows.Forms.Label();
            this.lblClaveSal = new System.Windows.Forms.Label();
            this.chkMedicamento = new System.Windows.Forms.CheckBox();
            this.chkCodigoEAN = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkDescomponer = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDirectorioDeImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
            this.FrameProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnCargarImagenes,
            this.separadorCargarImagenes,
            this.btnImg_Atras,
            this.toolStripSeparator2,
            this.btnImg_Adelante,
            this.toolStripSeparator1,
            this.btnDirectorioDeImagenes});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1456, 39);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(36, 36);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // btnCargarImagenes
            // 
            this.btnCargarImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCargarImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarImagenes.Image")));
            this.btnCargarImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCargarImagenes.Name = "btnCargarImagenes";
            this.btnCargarImagenes.Size = new System.Drawing.Size(36, 36);
            this.btnCargarImagenes.Text = "Agregar imagenes";
            this.btnCargarImagenes.ToolTipText = "Agregar imagenes";
            this.btnCargarImagenes.Click += new System.EventHandler(this.btnCargarImagenes_Click);
            // 
            // separadorCargarImagenes
            // 
            this.separadorCargarImagenes.Name = "separadorCargarImagenes";
            this.separadorCargarImagenes.Size = new System.Drawing.Size(6, 39);
            // 
            // btnImg_Atras
            // 
            this.btnImg_Atras.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImg_Atras.Image = ((System.Drawing.Image)(resources.GetObject("btnImg_Atras.Image")));
            this.btnImg_Atras.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImg_Atras.Name = "btnImg_Atras";
            this.btnImg_Atras.Size = new System.Drawing.Size(36, 36);
            this.btnImg_Atras.Text = "Imagen anterior";
            this.btnImg_Atras.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnImg_Atras.Click += new System.EventHandler(this.btnImg_Atras_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnImg_Adelante
            // 
            this.btnImg_Adelante.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImg_Adelante.Image = ((System.Drawing.Image)(resources.GetObject("btnImg_Adelante.Image")));
            this.btnImg_Adelante.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImg_Adelante.Name = "btnImg_Adelante";
            this.btnImg_Adelante.Size = new System.Drawing.Size(36, 36);
            this.btnImg_Adelante.Text = "Siguiente imagen";
            this.btnImg_Adelante.Click += new System.EventHandler(this.btnImg_Adelante_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.toolStrip1);
            this.FramePrincipal.Controls.Add(this.lblNombreImagen);
            this.FramePrincipal.Controls.Add(this.lblNdeM);
            this.FramePrincipal.Controls.Add(this.picImagen);
            this.FramePrincipal.Location = new System.Drawing.Point(787, 52);
            this.FramePrincipal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePrincipal.Size = new System.Drawing.Size(656, 516);
            this.FramePrincipal.TabIndex = 2;
            this.FramePrincipal.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMostrarTodasLasImagenes,
            this.toolStripSeparator5,
            this.btnRotarIzquierda,
            this.toolStripSeparator3,
            this.btnRotarDerecha,
            this.toolStripSeparator4,
            this.lblMensaje});
            this.toolStrip1.Location = new System.Drawing.Point(4, 19);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(648, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnMostrarTodasLasImagenes
            // 
            this.btnMostrarTodasLasImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMostrarTodasLasImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnMostrarTodasLasImagenes.Image")));
            this.btnMostrarTodasLasImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMostrarTodasLasImagenes.Name = "btnMostrarTodasLasImagenes";
            this.btnMostrarTodasLasImagenes.Size = new System.Drawing.Size(23, 22);
            this.btnMostrarTodasLasImagenes.Text = "Mostrar todas las imagenes";
            this.btnMostrarTodasLasImagenes.Click += new System.EventHandler(this.btnMostrarTodasLasImagenes_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRotarIzquierda
            // 
            this.btnRotarIzquierda.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotarIzquierda.Image = ((System.Drawing.Image)(resources.GetObject("btnRotarIzquierda.Image")));
            this.btnRotarIzquierda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotarIzquierda.Name = "btnRotarIzquierda";
            this.btnRotarIzquierda.Size = new System.Drawing.Size(23, 22);
            this.btnRotarIzquierda.Text = "Rotar a la izquierda";
            this.btnRotarIzquierda.ToolTipText = "Rotar a la izquierda";
            this.btnRotarIzquierda.Click += new System.EventHandler(this.btnRotarIzquierda_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRotarDerecha
            // 
            this.btnRotarDerecha.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotarDerecha.Image = ((System.Drawing.Image)(resources.GetObject("btnRotarDerecha.Image")));
            this.btnRotarDerecha.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotarDerecha.Name = "btnRotarDerecha";
            this.btnRotarDerecha.Size = new System.Drawing.Size(23, 22);
            this.btnRotarDerecha.Text = "Rotar a la derecha";
            this.btnRotarDerecha.ToolTipText = "Rotar a la derecha";
            this.btnRotarDerecha.Click += new System.EventHandler(this.btnRotarDerecha_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(232, 22);
            this.lblMensaje.Text = "Doble clic en la imagen ver zoom";
            // 
            // lblNombreImagen
            // 
            this.lblNombreImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreImagen.Location = new System.Drawing.Point(21, 460);
            this.lblNombreImagen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreImagen.MostrarToolTip = false;
            this.lblNombreImagen.Name = "lblNombreImagen";
            this.lblNombreImagen.Size = new System.Drawing.Size(620, 23);
            this.lblNombreImagen.TabIndex = 2;
            this.lblNombreImagen.Text = "Nombre de la Imagen";
            this.lblNombreImagen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNdeM
            // 
            this.lblNdeM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNdeM.Location = new System.Drawing.Point(21, 487);
            this.lblNdeM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNdeM.Name = "lblNdeM";
            this.lblNdeM.Size = new System.Drawing.Size(620, 23);
            this.lblNdeM.TabIndex = 5;
            this.lblNdeM.Text = "Imagen 1 de 1";
            this.lblNdeM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picImagen
            // 
            this.picImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImagen.Location = new System.Drawing.Point(19, 57);
            this.picImagen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picImagen.Name = "picImagen";
            this.picImagen.Size = new System.Drawing.Size(626, 393);
            this.picImagen.TabIndex = 0;
            this.picImagen.TabStop = false;
            this.picImagen.Click += new System.EventHandler(this.picImagen_Click);
            this.picImagen.DoubleClick += new System.EventHandler(this.picImagen_DoubleClick);
            // 
            // FrameProducto
            // 
            this.FrameProducto.Controls.Add(this.lblCancelado);
            this.FrameProducto.Controls.Add(this.txtClaveInternaSal);
            this.FrameProducto.Controls.Add(this.txtContenido);
            this.FrameProducto.Controls.Add(this.lblPresentacion);
            this.FrameProducto.Controls.Add(this.lblSegmento);
            this.FrameProducto.Controls.Add(this.lblSubFamilia);
            this.FrameProducto.Controls.Add(this.lblFamilia);
            this.FrameProducto.Controls.Add(this.lblClasificacionSSA);
            this.FrameProducto.Controls.Add(this.txtDescripcionCorta);
            this.FrameProducto.Controls.Add(this.txtDescripcion);
            this.FrameProducto.Controls.Add(this.lblTipoDeProducto);
            this.FrameProducto.Controls.Add(this.chkEsSectorSalud);
            this.FrameProducto.Controls.Add(this.label13);
            this.FrameProducto.Controls.Add(this.txtCodigoEAN);
            this.FrameProducto.Controls.Add(this.label12);
            this.FrameProducto.Controls.Add(this.lblLaboratorio);
            this.FrameProducto.Controls.Add(this.label14);
            this.FrameProducto.Controls.Add(this.lblDescripcionSal);
            this.FrameProducto.Controls.Add(this.lblClaveSal);
            this.FrameProducto.Controls.Add(this.chkMedicamento);
            this.FrameProducto.Controls.Add(this.chkCodigoEAN);
            this.FrameProducto.Controls.Add(this.label11);
            this.FrameProducto.Controls.Add(this.chkDescomponer);
            this.FrameProducto.Controls.Add(this.label10);
            this.FrameProducto.Controls.Add(this.label9);
            this.FrameProducto.Controls.Add(this.label8);
            this.FrameProducto.Controls.Add(this.label7);
            this.FrameProducto.Controls.Add(this.label6);
            this.FrameProducto.Controls.Add(this.label5);
            this.FrameProducto.Controls.Add(this.label4);
            this.FrameProducto.Controls.Add(this.label3);
            this.FrameProducto.Controls.Add(this.label2);
            this.FrameProducto.Location = new System.Drawing.Point(16, 52);
            this.FrameProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProducto.Size = new System.Drawing.Size(765, 516);
            this.FrameProducto.TabIndex = 1;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Datos Producto :";
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(555, 17);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(197, 25);
            this.lblCancelado.TabIndex = 36;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtClaveInternaSal
            // 
            this.txtClaveInternaSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtClaveInternaSal.Location = new System.Drawing.Point(152, 50);
            this.txtClaveInternaSal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtClaveInternaSal.Name = "txtClaveInternaSal";
            this.txtClaveInternaSal.Size = new System.Drawing.Size(197, 25);
            this.txtClaveInternaSal.TabIndex = 35;
            this.txtClaveInternaSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtContenido
            // 
            this.txtContenido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtContenido.Location = new System.Drawing.Point(660, 475);
            this.txtContenido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtContenido.MostrarToolTip = false;
            this.txtContenido.Name = "txtContenido";
            this.txtContenido.Size = new System.Drawing.Size(73, 28);
            this.txtContenido.TabIndex = 34;
            this.txtContenido.Text = "0";
            this.txtContenido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Location = new System.Drawing.Point(152, 444);
            this.lblPresentacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresentacion.MostrarToolTip = false;
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(600, 28);
            this.lblPresentacion.TabIndex = 14;
            this.lblPresentacion.Text = "scLabelExt1";
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSegmento
            // 
            this.lblSegmento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSegmento.Location = new System.Drawing.Point(152, 378);
            this.lblSegmento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSegmento.MostrarToolTip = false;
            this.lblSegmento.Name = "lblSegmento";
            this.lblSegmento.Size = new System.Drawing.Size(600, 28);
            this.lblSegmento.TabIndex = 12;
            this.lblSegmento.Text = "scLabelExt1";
            this.lblSegmento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubFamilia
            // 
            this.lblSubFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubFamilia.Location = new System.Drawing.Point(152, 342);
            this.lblSubFamilia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubFamilia.MostrarToolTip = false;
            this.lblSubFamilia.Name = "lblSubFamilia";
            this.lblSubFamilia.Size = new System.Drawing.Size(600, 28);
            this.lblSubFamilia.TabIndex = 11;
            this.lblSubFamilia.Text = "scLabelExt1";
            this.lblSubFamilia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFamilia
            // 
            this.lblFamilia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFamilia.Location = new System.Drawing.Point(152, 310);
            this.lblFamilia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFamilia.MostrarToolTip = false;
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(600, 28);
            this.lblFamilia.TabIndex = 10;
            this.lblFamilia.Text = "scLabelExt1";
            this.lblFamilia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClasificacionSSA
            // 
            this.lblClasificacionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClasificacionSSA.Location = new System.Drawing.Point(152, 278);
            this.lblClasificacionSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClasificacionSSA.MostrarToolTip = false;
            this.lblClasificacionSSA.Name = "lblClasificacionSSA";
            this.lblClasificacionSSA.Size = new System.Drawing.Size(600, 28);
            this.lblClasificacionSSA.TabIndex = 9;
            this.lblClasificacionSSA.Text = "scLabelExt1";
            this.lblClasificacionSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescripcionCorta
            // 
            this.txtDescripcionCorta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDescripcionCorta.Location = new System.Drawing.Point(152, 218);
            this.txtDescripcionCorta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtDescripcionCorta.MostrarToolTip = false;
            this.txtDescripcionCorta.Name = "txtDescripcionCorta";
            this.txtDescripcionCorta.Size = new System.Drawing.Size(600, 28);
            this.txtDescripcionCorta.TabIndex = 6;
            this.txtDescripcionCorta.Text = "scLabelExt1";
            this.txtDescripcionCorta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDescripcion.Location = new System.Drawing.Point(152, 186);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtDescripcion.MostrarToolTip = false;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(600, 28);
            this.txtDescripcion.TabIndex = 5;
            this.txtDescripcion.Text = "scLabelExt1";
            this.txtDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTipoDeProducto
            // 
            this.lblTipoDeProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoDeProducto.Location = new System.Drawing.Point(152, 154);
            this.lblTipoDeProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoDeProducto.MostrarToolTip = false;
            this.lblTipoDeProducto.Name = "lblTipoDeProducto";
            this.lblTipoDeProducto.Size = new System.Drawing.Size(600, 28);
            this.lblTipoDeProducto.TabIndex = 4;
            this.lblTipoDeProducto.Text = "scLabelExt1";
            this.lblTipoDeProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkEsSectorSalud
            // 
            this.chkEsSectorSalud.AutoSize = true;
            this.chkEsSectorSalud.Location = new System.Drawing.Point(493, 252);
            this.chkEsSectorSalud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEsSectorSalud.Name = "chkEsSectorSalud";
            this.chkEsSectorSalud.Size = new System.Drawing.Size(154, 21);
            this.chkEsSectorSalud.TabIndex = 8;
            this.chkEsSectorSalud.Text = "Es del Sector Salud";
            this.chkEsSectorSalud.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(11, 23);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 16);
            this.label13.TabIndex = 33;
            this.label13.Text = "EAN :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(152, 18);
            this.txtCodigoEAN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigoEAN.MaxLength = 15;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(196, 22);
            this.txtCodigoEAN.TabIndex = 0;
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoEAN_KeyDown);
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(11, 384);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Segmento :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.Location = new System.Drawing.Point(152, 415);
            this.lblLaboratorio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(600, 23);
            this.lblLaboratorio.TabIndex = 13;
            this.lblLaboratorio.Text = "label12";
            this.lblLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(11, 50);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 26);
            this.label14.TabIndex = 28;
            this.label14.Text = "Clave Interna SSA :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSal.Location = new System.Drawing.Point(152, 80);
            this.lblDescripcionSal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(600, 68);
            this.lblDescripcionSal.TabIndex = 3;
            this.lblDescripcionSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClaveSal
            // 
            this.lblClaveSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSal.Location = new System.Drawing.Point(555, 50);
            this.lblClaveSal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClaveSal.Name = "lblClaveSal";
            this.lblClaveSal.Size = new System.Drawing.Size(197, 25);
            this.lblClaveSal.TabIndex = 2;
            this.lblClaveSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMedicamento
            // 
            this.chkMedicamento.AutoSize = true;
            this.chkMedicamento.Location = new System.Drawing.Point(152, 252);
            this.chkMedicamento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMedicamento.Name = "chkMedicamento";
            this.chkMedicamento.Size = new System.Drawing.Size(207, 21);
            this.chkMedicamento.TabIndex = 7;
            this.chkMedicamento.Text = "Es Medicamento Controlado";
            this.chkMedicamento.UseVisualStyleBackColor = true;
            // 
            // chkCodigoEAN
            // 
            this.chkCodigoEAN.AutoSize = true;
            this.chkCodigoEAN.Location = new System.Drawing.Point(308, 480);
            this.chkCodigoEAN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCodigoEAN.Name = "chkCodigoEAN";
            this.chkCodigoEAN.Size = new System.Drawing.Size(156, 21);
            this.chkCodigoEAN.TabIndex = 16;
            this.chkCodigoEAN.Text = "Maneja Código EAN";
            this.chkCodigoEAN.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(508, 481);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Contenido paquete :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDescomponer
            // 
            this.chkDescomponer.AutoSize = true;
            this.chkDescomponer.Location = new System.Drawing.Point(152, 480);
            this.chkDescomponer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDescomponer.Name = "chkDescomponer";
            this.chkDescomponer.Size = new System.Drawing.Size(95, 21);
            this.chkDescomponer.TabIndex = 15;
            this.chkDescomponer.Text = "Componer";
            this.chkDescomponer.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 450);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Presentación :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 417);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 16);
            this.label9.TabIndex = 19;
            this.label9.Text = "Laboratorio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 351);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sub-Familia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 318);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Familia :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 160);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tipo de producto :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 284);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Clasificación :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 224);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Descripción corta :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(461, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clave SSA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 192);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDirectorioDeImagenes
            // 
            this.btnDirectorioDeImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDirectorioDeImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorioDeImagenes.Image")));
            this.btnDirectorioDeImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDirectorioDeImagenes.Name = "btnDirectorioDeImagenes";
            this.btnDirectorioDeImagenes.Size = new System.Drawing.Size(36, 36);
            this.btnDirectorioDeImagenes.Text = "Directorio de imagenes";
            this.btnDirectorioDeImagenes.ToolTipText = "Directorio de imagenes";
            this.btnDirectorioDeImagenes.Click += new System.EventHandler(this.btnDirectorioDeImagenes_Click);
            // 
            // FrmProducto_KarruselImagenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 580);
            this.Controls.Add(this.FrameProducto);
            this.Controls.Add(this.FramePrincipal);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmProducto_KarruselImagenes";
            this.Text = "Galeria de imagenes de producto";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProducto_KarruselImagenes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmProducto_KarruselImagenes_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
            this.FrameProducto.ResumeLayout(false);
            this.FrameProducto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnImg_Atras;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImg_Adelante;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private System.Windows.Forms.PictureBox picImagen;
        private System.Windows.Forms.Label lblNdeM;
        private SC_ControlsCS.scLabelExt lblNombreImagen;
        private System.Windows.Forms.GroupBox FrameProducto;
        private System.Windows.Forms.CheckBox chkEsSectorSalud;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblLaboratorio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblDescripcionSal;
        private System.Windows.Forms.Label lblClaveSal;
        private System.Windows.Forms.CheckBox chkMedicamento;
        private System.Windows.Forms.CheckBox chkCodigoEAN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkDescomponer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scLabelExt lblTipoDeProducto;
        private SC_ControlsCS.scLabelExt txtDescripcion;
        private SC_ControlsCS.scLabelExt txtDescripcionCorta;
        private SC_ControlsCS.scLabelExt lblSubFamilia;
        private SC_ControlsCS.scLabelExt lblFamilia;
        private SC_ControlsCS.scLabelExt lblClasificacionSSA;
        private SC_ControlsCS.scLabelExt lblSegmento;
        private SC_ControlsCS.scLabelExt lblPresentacion;
        private SC_ControlsCS.scLabelExt txtContenido;
        private System.Windows.Forms.Label txtClaveInternaSal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.ToolStripButton btnCargarImagenes;
        private System.Windows.Forms.ToolStripSeparator separadorCargarImagenes;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRotarIzquierda;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRotarDerecha;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel lblMensaje;
        private System.Windows.Forms.ToolStripButton btnMostrarTodasLasImagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnDirectorioDeImagenes;
    }
}