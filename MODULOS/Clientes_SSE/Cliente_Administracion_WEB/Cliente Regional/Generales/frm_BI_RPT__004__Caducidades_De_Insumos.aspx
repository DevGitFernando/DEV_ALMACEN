<%@ Page Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeFile="frm_BI_RPT__004__Caducidades_De_Insumos.aspx.cs" Inherits="frm_BI_RPT__004__Caducidades_De_Insumos" %>
<asp:Content ID="localCss" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css" id="styleSection"></style>
    <%--<link href="../Content/AdaptiveGridLayout.css" rel="stylesheet" />
    <script src="../Scripts/AdaptiveGridLayout.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <%--The current form layout width is
    <dx:ASPxLabel runat="server" ID="CurrentWidthLabel" ClientInstanceName="CurrentWidthLabel" Font-Bold="true"></dx:ASPxLabel>
    pixels.<br />
    The current break point is set to
    <dx:ASPxLabel runat="server" ID="CurrentBreakpoint" ClientInstanceName="CurrentBreakpointLabel" Font-Bold="true"></dx:ASPxLabel>
    .<br />
    All form editors are displayed within
    <dx:ASPxLabel runat="server" ID="CurrentColCountLabel" ClientInstanceName="CurrentColCountLabel" Font-Bold="true"></dx:ASPxLabel>
    columns.<br />
    The captions are
    <dx:ASPxLabel runat="server" ID="CurrentWrapLabel" ClientInstanceName="CurrentWrapLabel" Font-Bold="true"></dx:ASPxLabel>
    .<br />
    <br />--%>
    <dx:ASPxMenu ID="Menu" ClientInstanceName="Menu" runat="server" ShowAsToolbar="true" ShowPopOutImages="true" Width="" 
         CssClass="FrmToolbar" OnItemClick="Menu_ItemClick" EnableCallBacks="true">
        <Items>
           <dx:MenuItem BeginGroup="true" Name="New" Text="Nuevo">
                <Image IconID="actions_new_32x32"></Image>
            </dx:MenuItem>
            <dx:MenuItem BeginGroup="true" Name="Save" Text="Ejecutar">
                <Image IconID="save_save_32x32"></Image>
            </dx:MenuItem>
        </Items>
    </dx:ASPxMenu>
    <h2 class="EncFormTitle">  Caducidad de medicamento o material de curación</h2>
    <div id="ResizedDiv" style="width: 100%;">
        <dx:ASPxFormLayout runat="server" ID="Caducidad1" Width="100%" ClientInstanceName="FormLayout">
            <Items>
                <dx:LayoutGroup Width="100%" Caption="Información de Unidad:" ColumnCount="5">
                    <GridSettings StretchLastItem="true" ChangeCaptionLocationAtWidth="660">
                        <Breakpoints>
                            <dx:LayoutBreakpoint MaxWidth="500" ColumnCount="1" Name="S" />
                            <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="2" Name="M" />
                        </Breakpoints>
                    </GridSettings>
                    <Items>
                        <dx:LayoutItem Caption="Tipo de Unidad" ColumnSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cboUnidad" ClientInstanceName="cboUnidad" EnableCallbackMode="true"
                                        ValueType="System.String" ValueField="IdTipoUnidad" TextFormatString="{0} -- {1}"
                                        DropDownStyle="DropDown" Width="100%" OnSelectedIndexChanged="cboUnidad_SelectedIndexChanged" AutoPostBack="true">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="IdTipoUnidad" />
                                            <dx:ListBoxColumn FieldName="TipoDeUnidad" />
                                        </Columns>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { e.processOnServer=true; }" />
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="Periodo de revisión" VerticalAlign="Middle" ColumnSpan="1">
                            </dx:LayoutItem>

                        <dx:LayoutItem Caption="Jurisdicción" ColumnSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cboJurisdiccion" ClientInstanceName="cboJurisdiccion" EnableCallbackMode="false"
                                        ValueType="System.String" ValueField="IdJurisdiccion" TextFormatString="{0} -- {1}"
                                        DropDownStyle="DropDown" Width="100%" OnSelectedIndexChanged="cboJurisdiccion_SelectedIndexChanged" AutoPostBack="true">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="IdJurisdiccion" />
                                            <dx:ListBoxColumn FieldName="Jurisdiccion" />
                                        </Columns>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { e.processOnServer=true; }" />
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                          <dx:LayoutItem Caption="Fecha" VerticalAlign="Middle" ColumnSpan="2">
                            <SpanRules>
                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                            </SpanRules>
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit runat="server" ID="DtFecha" Width="100%">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Localidad" ColumnSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cboLocalidad" ClientInstanceName="cboLocalidad" EnableCallbackMode="false"
                                        ValueType="System.String" ValueField="IdMunicipio" TextFormatString="{0} -- {1}"
                                        DropDownStyle="DropDown" Width="100%" OnSelectedIndexChanged="cboLocalidad_SelectedIndexChanged" AutoPostBack="true">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="IdMunicipio" />
                                            <dx:ListBoxColumn FieldName="Municipio" />
                                        </Columns>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { e.processOnServer=true; }" />
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                         <dx:LayoutItem Caption="Farmacia" ColumnSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxComboBox runat="server" ID="cboFarmacia" ClientInstanceName="cboFarmacia" EnableCallbackMode="false"
                                        ValueType="System.String" ValueField="IdFarmacia" TextFormatString="{0} -- {1}"
                                        DropDownStyle="DropDown" Width="100%" AutoPostBack="false">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="IdFarmacia" />
                                            <dx:ListBoxColumn FieldName="Farmacia" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>

        <dx:ASPxFormLayout runat="server" ID="Caducidad2" Width="100%" ClientInstanceName="FormLayout">
            <Items>
                <dx:LayoutGroup Width="100%" Caption="Parámetros:" ColumnCount="5">
                    <GridSettings StretchLastItem="true" ChangeCaptionLocationAtWidth="660">
                        <Breakpoints>
                            <dx:LayoutBreakpoint MaxWidth="500" ColumnCount="1" Name="S" />
                            <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="2" Name="M" />
                        </Breakpoints>
                    </GridSettings>
                    <Items>
                        <dx:LayoutItem Caption="Semaforización" ColumnSpan="3">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                   <dx:ASPxComboBox runat="server" ID="cboSemafo" ClientInstanceName="cboSemafo" EnableCallbackMode="false"
                                        ValueType="System.String" ValueField="IdSemaforizacion" TextFormatString="{0} -- {1}"
                                        DropDownStyle="DropDown" Width="100%">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                       <Items>
                                           <dx:ListEditItem Text="Todos" Value="0"  Selected="true"/>
                                           <dx:ListEditItem Text="Proximos a caducar" Value="1" />
                                           <dx:ListEditItem Text="Mediana Caducidad" Value="2" />
                                           <dx:ListEditItem Text="Larga Caducidad" Value="3" />
                                       </Items>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <%--<dx:LayoutItem Caption="Semaforización" ColumnSpan="3">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox runat="server" ID="cboSemafo" ClientInstanceName="cboSemafo" EnableCallbackMode="false"
                                            ValueType="System.String" ValueField="Id" TextFormatString="{0} - {1}"
                                            DropDownStyle="DropDown" Width="100%">
                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                            <Columns>
                                                <dx:ListBoxColumn FieldName="Prueba1" />
                                                <dx:ListBoxColumn FieldName="Prueba2" />
                                            </Columns>
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnPersonalChanged(s); }" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>--%>
                        <dx:LayoutItem Caption="Procedencia" ColumnSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="txtProcedencia" ClientInstanceName="txtProcedencia" SpinButtons-ClientVisible="false">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="MyForm">
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
            <dx:ASPxHint ID="ASPxHint" runat="server"></dx:ASPxHint>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
        </dx:ASPxGlobalEvents>
        <%--<ef:EntityDataSource ID="CountriesDataSource" runat="server" ContextTypeName="DevExpress.Web.Demos.DataContext" EntitySetName="countries" />--%>
    </div>

    <div id="ResizedDiv2" style="width: 100%;">
        <dx:ASPxSplitter ID="FrameResult" ClientInstanceName="FrameResult" runat="server" Height="400px">
            <Panes>
                <dx:SplitterPane Name="ContentUrlPane" ScrollBars="Auto" ContentUrlIFrameName="contentUrlPane">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
    </div>
</asp:Content>
