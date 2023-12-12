using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_IMach4.Protocolos
{
    #region Protocolo 3964R 
    public enum NEMONICOS
    {
        NULO = 0,
        SOH = 1,
        STX = 2,
        ETX = 3,
        EOT = 4,
        DLE = 16,
        NAK = 21
    }

    public enum Dialogos
    {
        None = 0,
        a = 97, A = 65,
        b = 98, B = 66,
        f = 102, F = 70,
        g = 103, G = 71,
        i = 105, I = 73,
        k = 107, K = 75,
        m = 109, M = 77,
        o = 111, O = 79,
        p = 112, P = 80,
        r = 114, R = 82,
        s = 115, S = 83
    }
    #endregion Protocolo 3964R
    
    public enum IMach4_PrioridaProceso
    {
        Nivel_1 = 1, Nivel_2 = 2, Nivel_3 = 3, Nivel_4 = 4, Nivel_5 = 5
    }

    public enum IMach4_StatusRespuesta_A
    {
        Busy = 0, In_Queue = 1, Queue_Full = 2, Cancelled = 3,
        AcknowledgmentMessage = 4, Quantity_Change_From_MEDIMAT_to_ERP = 5
    }

    public enum IMach4_I_OrdersState
    {
        None = -1, 
        AllowToStore = 0, 
        NotAllowToStore = 1, 
        AllowToStoreWithUseByDate = 2, 
        NoSettingOfMaintenanceOfBachCode = 4, 
        StoreProductoInFridge = 5 
    }

    public enum IMach4_i_State
    {
        None = -1, 
        EntryOfGoods = 0, 
        InternalRearrangementsOfGoods = 1, 
        StartNewDelivery = 2, 
        EndOfDelivery = 3, 
        EntryOfGoodsWithMaintenanceOfTheBachCoding = 4, 
        InternalRearrangementOfGoodsWithMaintenanceOfBachCoding = 5, 
        ConfirmationPackageStored = 6, 
        ConfirmationPackageNotStored = 7 
    }
}
