<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRecetario.aspx.cs" Inherits="DllRecetario_FrmRecetario" %>

<!doctype html>
<html lang="es">
<head>
	<meta charset="utf-8">
    <link rel="stylesheet" href="../css/normalize.css">
    <link rel="stylesheet" type="text/css" href="../assets/DataTables/datatables.min.css"/>
    <link rel="stylesheet" href="../css/componentes.css">
    <link rel="stylesheet" href="../css/ayuda.css">
    <link rel="stylesheet" href="../css/recetario.css">
    

    <title>Recetario</title>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
    </script>

</head>

<body>
	
   <!-- <div class="maskloader">
    	<div class="caja">
        	<div class="msjLoader">Cargando Información, espere por favor...</div>
        </div>
    </div>-->
    
    <form id="FrmRecetario" runat="server">
	<div id="container"> <!--Contenedor General-->
    
    	<section id="Content_form">
        	<div class="title_form">Receta Electrónica</div>
            <div class="infopaciente">
            
        		<div class="tituloInfo">Datos generales del paciente

                    <div class="tipoconsulta">
	                <div class="question">Seleccione el tipo de Consulta</div>
                   
                    <div class="btnRadio">
                         <div>
                        <input type="radio" name="radio" id="radio1" class="radio" checked/>
                        <label for="radio1">Membresía</label>
                        </div>
                        
                        <div>
                        <input type="radio" name="radio" id="radio2" class="radio"/>
                        <label for="radio2">Particular</label>
                        </div>
					
                    </div>
                </div>
                
                
                </div> 
                  
				 
				<!------------Datos paciente-------------->
                
                 <!--Linea1-->
                <div class=" labelGeneral alineado spacelabel">Nº Afiliado</div>
                <input id="txtcodAfiliado" class="Nafiliado" type="text" placeholder="N° de afiliado" name="txtcodAfiliado" value="" />
                <input class="Nafiliado2" type="text" placeholder="0"name="nombre" value="" />
                
                <div class=" labelGeneral alineado spacelabel">Elegibilidad</div>
                <input class="elegibilidad" type="text" placeholder="0"name="nombre" value="" />
                <div class=" labelGeneral alineado spacelabel">Folio</div>
                <input class="folio" type="text" placeholder="0"name="nombre" value="" />
                
                                <!--Linea2-->
                <div class=" labelGeneral alineado spacelabel">Nombre</div>
                <input class="npaciente1" type="text" placeholder="Introduzca el nombre de Usuario" name="nombre" value="" />
                <div class=" labelGeneral alineado spacelabel">Empresa</div>
                
                <input class="npaciente2" type="text" placeholder="0" name="nombre" value="" />
				<input class="npaciente3" type="text" placeholder="Nombre de la empresa" name="nombre" value="" />
                
                                <!--Linea2-->
                <div class=" labelGeneral alineado spacelabel">Plan y producto</div>
                <input class="plan1" type="text" placeholder="0" name="nombre" value="" />
                <input class="plan2" type="text" placeholder="Producto"name="nombre" value="" />
                <input class="plan3" type="text" placeholder="0" name="nombre" value="" />
                <input class="plan4" type="text" placeholder="Plan" name="nombre" value="" />
                
                <input class="CodPeriodo" type="hidden" placeholder="CodPeriodo" value="" />
                
                <!--Botones Expediente electrónico y cobertura-->
              <%-- <div class="contBtnPaciente">
                    <div class="btnGeneral btnelegibilidad alineado spacelabel">Cobertura</div>
                    <div class="btnGeneral btnexpediente alineado spacelabel">Exp. Electrónica</div>
                </div>--%>
                   
               </div>
            
            

            <!----------Consulta------------->
           <%-- <div class="tituloInfo consultaspace">Consulta</div>
            <div id="consultainfo" class="consultainfo" runat="server">
            	<div class="labelGeneral spacelabel alineado"> Medico tratante</div>
                   <select id="cboMedico" class="nmedico alineado" name="Estado">
                    <option>Juan Carlos Fernandez Gonzalez</option>
                    <option>Enrique Guerrero</option>
	               </select>
                   
                   
                  
                  <div  id="btnaboratorio" class=" alineado spacelabel">Laboratorio</div>

                <input id="Laboratorio" type="text" placeholder="11" name="nombre" value="" />
                   
            </div>--%>
            <!----------Laboratorio------------->
            

            <!----------Diagnostico------------->
            <div class="titulodiag">Diangnóstico y Procedimientos</div>
            <div class="consultainfo">
            	<div class="contdiag">
                    <div class="contCie10">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="clavediag" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcion" type="text" placeholder="CIE"name="nombre" value="" />
                    </div>

                    <div class="contCie10">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="clavediag" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcion" type="text" placeholder="CIE"name="nombre" value="" />
                    </div>

                    <div class="contCie10">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="clavediag" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcion" type="text" placeholder="CIE"name="nombre" value="" />
                    </div>
                            <%--<div class="btnNuevoDiag spacelabel alineado">Nuevo diagnóstico <strong>+</strong></div>--%>
                </div>

                <div class="contProcedimientos">
                   
                    <div class="contProcedAdd">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="claveProced" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcionProced" type="text" placeholder="Procedimiento"name="nombre" value="" />
                        <input class="monto" type="hidden" placeholder="monto" value="" />
                    </div>
                    <div class="contProcedAdd">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="claveProced" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcionProced" type="text" placeholder="Procedimiento"name="nombre" value="" />
                        <input class="monto" type="hidden" placeholder="monto" value="" />
                    </div>
                    <div class="contProcedAdd">
            		    <div class="labelGeneral"> Clave</div>
                        <input class="claveProced" type="text" placeholder="Clave"name="nombre" value="" />
                        <div class="labelGeneral spacelabel alineado">Descripción</div>
                        <input class="descripcionProced" type="text" placeholder="Procedimiento"name="nombre" value="" />
                        <input class="monto" type="hidden" placeholder="monto" value="" />
                    </div>
                </div>
               
                <%--<div class="contObservaciones">
                    <textarea  class="observaciones" name="comentarios" placeholder="Escriba aquí su Nota Médica" rows="" cols=""></textarea>
                </div>--%>
            
            </div>
            
            
            <!----------Contenedor Clave------------->
            <div class="tituloInfo">Claves</div>
            <div id="contClavesGeneral">
            	<div class="ContBotonesClave">
                	<div id="btnAgregar"></div>
					<div id="btnBorrar"></div>
                </div>
                <div id="containerTableClave">
                    <table id="TableProductos" class="table table-striped table-bordered table-hover" cellspacing="0" width="100%">
                        <thead class="thead">
                            <tr>
                                <th>Id</th>
                                <th>Descripción</th>
                                <th>Indicaciones</th>
                                <th>Cantidad</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
               
            </div>
            
            
            
            
            <!--NotaMedica-->
            
            	<div class="tituloNotamed">Nota médica</div>
            <div class="contNotamedica">
                <textarea id="txtNotaMedicas" class="txtNotamedica" placeholder="Escriba aquí sus anotaciones" rows="" cols=""></textarea>
                
                <div id="btnGuardar" class="btnGuardaractive">Guardar</div>
            </div>
            
            
            
            <!----------Observaciones------------->

        </section>
	</div>
    <!--END contenedor-->
    
    
    
	<!--Buscar Afiliado-->
    		<div id="BusquedaAfiliado" class="mask">
                <div class="contentAyuda elementCenter">
                        <div class="headerAyuda">Busque un Afiliado
                            <div class="btnClose"></div>
                        </div>

                        <div class="datosb">
                            <div class="labelGeneral">N° Afiliado</div>
                            <input id="txtSearchCodAfiliado"  class="search" type="text" name="search" placeholder="Codigo de Afiliado">
                            <div id="btnBuscarAfiliado" class="btnclick btnAyuda">Buscar</div>
                            <div id="btnLimpiarAfiliado" class="btnclick btnAyuda">Limpiar</div>
                            
                            <input id="txtSearchApPaterno" class="nombreBusqueda" type="text" name="search" placeholder="Apellido Paterno" maxlength="250">
                            <input id="txtSearchApMaterno" class="nombreBusqueda" type="text" name="search" placeholder="Apellido Materno" maxlength="250">
                            <input id="txtSearchNombre" class="nombreBusqueda" type="text" name="search" placeholder="Nombre" maxlength="250">
                       </div>
                       <div class="TableAfiliado"></div>
                       <div id="btnAddAfiliado" class="btnAdd">Agregar</div>
                  </div>
            </div>



   
            
            
       <!--Ventana Buscar clave-->
        
            <div id="BusquedaCIE" class="mask">
                <div id="claveCie" class="contentCie elementCenter">
                        <div class="headerCie">Busque una Clave
                            <div class="btnClose"></div>
                        </div>
                        <div class="datosCie">
                            
                            <div class="labelGeneral">Clave CIE</div>
                            <input id="txtSearchCIE" class="search" type="text" name="search" placeholder="Escriba la descripción de la clave CIE">
                            <div id="btnBuscarCIE" class="btnclick btnAyuda">Buscar</div>
                            <div id="btnLimpiarCIE" class="btnclick btnAyuda">Limpiar</div>
                                    
                    </div>
                    <div class="TableClaveCie"></div>
                    <div id="btnAddCIE" class="btnAdd">Agregar</div>
                </div>
            </div>
            
            
            
            
            <!---Ventan Claves--->
            
            <div id="BusquedaProducto" class="mask">
                <div id="clave" class="contentClaveAyuda elementCenter">
                        <div class="headerClaveAyuda">Busque una Clave
                            <div class="btnClose"></div>
                        </div>
                        <div class="datosClaveAyuda">
                            
                            <div class="labelGeneral">Clave</div>
                            <input id="txtSearchProducto" class="search" type="text" name="search" placeholder="Escriba la descripción de la clave">
                            <div id="btnBuscarProducto" class="btnclick btnAyuda">Buscar</div>
                            <div id="btnLimpiarProducto" class="btnclick btnAyuda">Limpiar</div>
                                    
                    </div>
                    <div class="TableClaveAyuda"></div>
                    <div id="btnAddProducto" class="btnAdd">Agregar</div>
                </div>
            </div>
            
           <!---Ventan Claves--->




           <!--BusqueLaboratorio-->
            <div id="BusquedaLaboratorio" class="mask">
                <div id="AddLab" class="contentLaboratorio elementCenter">
                    <div class="headerLaboratorio">Busque un laboratorio
                        <div class="btnClose"></div>
                    </div>

                    <div class="contTypeLab">
                        <div class="btnRadio">
                             <div>
                                <input type="radio" name="typelab" id="gabinete" class="radio" value="0" checked/>
                                <label for="gabinete">Gabinete</label>
                            </div>
                        
                            <div>
                                <input type="radio" name="typelab" id="laboratorio" value="1"  class="radio"/>
                                <label for="laboratorio">Laboratorio</label>
                            </div>
					    </div>
                    </div>

                    <div class="laboratorio">
                        <div class="labelGeneral">Laboratorio</div>
                        <input id="txtSearchLabGab" class="search" type="text" name="search" placeholder="Escriba la descripción del laboratorio o gabinete">
                        <div id="btnBuscarLab" class="btnclick btnAyuda">Buscar</div>
                        <div id="btnLimpiarLab" class="btnclick btnAyuda">Limpiar</div>
                    </div>

                    <div class="tituloInfo">Resultados de Busqueda</div>
                    <div class="TableLab1"></div>
                     
                    <div class="contBtnLab">
                        <span id="btnAddLaboratorio" class="btnAddLab">Agregar</span>
                    </div>
                    <div class="tituloInfo">Lista de laboratorios Agregados</div>
                    <div class="TableLab2"></div>
                    <div class="contBtnLab">
                        <span id="btnRemoveProcedimiento" class="btnRemove">Eliminar</span>
                    </div>
                     
                </div>
            </div>
              <!--BusqueLaboratorio-->


   
           <!--Venta procedimientos-->
            <div id="BusquedaProcedimientos" class="mask">
                <div class="contentProcedimientos elementCenter">
                    <div class="headerAyuda">Busque un procedimiento
                        <div class="btnClose"></div>
                    </div>
                    <div class="datosProcedimientos">
                        <div class="labelGeneral">Consulta</div>
                        <input id="txtSearchProcedimiento" class="search" type="text" name="search" placeholder="Busque un procedimiento">
                        <div id="btnBuscarProcedimiento" class="btnclick btnAyuda">Buscar</div>
                        <div id="btnLimpiarProcedimiento" class="btnclick btnAyuda">Limpiar</div>
                    </div>
                    <%--<div class="tituloInfo">Lista de procedimientos</div>--%>
                    <div class="TableProcedimientos1"></div>
                    <%--<div class="tituloInfo">Lista de procedimientos agregados</div>
                    <div class="TableProcedimientos2"></div>--%>
                    <div id="btnAddProcedimiento" class="btnAdd">Agregar</div>

           		</div>
            </div>
           <!--Venta procedimientos-->

    </form>
    </body>
</html>
