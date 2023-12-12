<?php
header("Content-Type: text/html;charset=utf-8");
include('lib/nusoap.php');
$client = new nusoap_client('http://intermed-queretaro.homeip.net/SERVICIOS/SESEQ/wsRecepcionRecetasColectivos/wsInterfaceISESEQ.asmx?wsdl',true);
$param= array('Informacion_XML'=>'{
	"general":
	[
		{
			"claveUnidadMedica":"QTSSA001752", 
			"NombreUnidadMedica":"HOSPITAL DE ESPECIALIDADES DEL NINO Y LA MUJER",
			"FolioDeReceta":"HENM-0001\/2019",
			"FechaDeReceta":"2019-08-16 09:07:26",
			"Origen":"1",
			"servicio":"CIRUG\u00cdA",
			"cie10":"J301",
			"FolioDeReferencia":"Seguro_Popular",
			"Vigencia":"2019-08-23",
			"NombreDelBeneficiario":"JUAN CARLOS LOPEZ PEREZ",
			"sexo":"Masculino",
			"fechaDeNacimiento":"2000-01-01",

			"NombreDeQuienPrescribe":"IVAN ALEJANDRO LEO UGALDE",
			"CedulaDeQuienPrescribe":"10038175"
		}, 
		{
			"claveUnidadMedica":"QTSSA001752",
			"NombreUnidadMedica":"HOSPITAL DE ESPECIALIDADES DEL NINO Y LA MUJER",
			"FolioDeReceta":"HENM-0002\/2019",
			"FechaDeReceta":"2019-08-19 12:35:00",
			"Origen":"1",
			"servicio":"CIRUG\u00cdA",
			"cie10":"A09",
			"FolioDeReferencia":"2250010009-01",
			"Vigencia":"2022-08-23",
			"NombreDelBeneficiario":"FRANCISCO CONTRERAS",
			"sexo":"Masculino",
			"fechaDeNacimiento":"1990-03-16",

			"NombreDeQuienPrescribe":"IVAN ALEJANDRO LEO UGALDE",
			"CedulaDeQuienPrescribe":"10038175"
		}
	], 

	"insumos":
	[
		{
			"FolioDeReceta":"HENM-0001\/2019",
			"ClaveInsumo":"25331.010-000-0104-00",
			"CantidadRequerida":"2"
		},
		{
			"FolioDeReceta":"HENM-0001\/2019",
			"ClaveInsumo":"25331.010-000-4176-00",
			"CantidadRequerida":"1"
		},
		{
			"FolioDeReceta":"HENM-0001\/2019",
			"ClaveInsumo":"25331.010-000-0101-00",
			"CantidadRequerida":"2"
		},
		{
			"FolioDeReceta":"HENM-0002\/2019",
			"ClaveInsumo":"25331.010-000-1206-00",
			"CantidadRequerida":"1"
		},
		{
			"FolioDeReceta":"HENM-0002\/2019",
			"ClaveInsumo":"25331.010-000-0402-00",
			"CantidadRequerida":"3"
		}
	]
}');

$result = $client->call('RecepcionDeRecetaElectronica', $param);
print_r($result);
print_r($result['RecepcionDeRecetaElectronicaResult']);
//print_r($result['RecepcionDeRecetaElectronicaResult']=>['Foliodereceta']);