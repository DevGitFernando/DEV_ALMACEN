<%@ Page Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeFile="frm_BI_UNI_RPT__001__ClavesSSA__Unidosis.aspx.cs" Inherits="Generales_frm_BI_UNI_RPT__001__ClavesSSA__Unidosis" %>
<asp:Content ID="localCss" ContentPlaceHolderID="Head" runat="server">
    <style type="text/css" id="styleSection"></style>
<%--    <link href="../Content/AdaptiveGridLayout.css" rel="stylesheet" />
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
    <h2 class="EncFormTitle">  Listado de medicamentos en Unidosis</h2>
    <div id="ResizedDiv" style="width: 100%;">
        
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
