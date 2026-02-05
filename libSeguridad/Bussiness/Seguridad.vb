Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Security
Imports System.Security.Principal

Public Class Seguridad
    'Funciones por Roles

    ' ***  SERVICIO 2 - EXTRACTOS: ***
    ' 20001-Consultar Extractos
    ' 20002-Imprimir Extractos
    ' 20003-Guardar Extractos
    ' 20004-Autorizar Extractos


    '***  SOLICITUD ***
    '09-Consultar Solic. propias        
    '02-Alta Solic. propias             
    '12-Editar Solic. propias           
    '06-Cancelar Solic. propias         
    '16-Ver Detalle Solic. propia       
    '08-Consultar Solic. todas          
    '01-Alta Solic. todas               
    '11-Editar Solic. todas             
    '05-Cancelar Solic. todas           
    '15-Ver Detalle Solic. todas        
    '07-Confirmar solicitudes           
    '72-Exportar Solicitudes            

    '***  EVALUACION ***
    '20-Consultar Evaluacion            
    '21-Iniciar Evaluacion              
    '19-Cerrar evaluacion
    '22-Reabrir evaluacion
    '18-Calificar solicitud
    '23-Solicitar intervencion de asistente
    '24-Solicitar intervencion de experto
    '25-Solicitar intervencion de inspector

    '***  INFORME ***
    '66-Agregar informe adjunto
    '67-Agregar informe automatico
    '68-Eliminar informe adjunto
    '69-Eliminar informe automatico
    '70-Obtener copia de informe
    '71-Ver informe

    '***  COMENTARIOS ***
    '60-Agregar comentarios
    '60-Agregar comentarios
    '61-Agregar comentarios propios
    '62-Consultar comentarios
    '63-Consultar comentarios propios
    '64-Eliminar comentarios
    '65-Eliminar comentarios propios

    '***  ANALISIS CUANTITATIVO ***
    '45-Administrar comentarios analisis cuantitativo
    '46-Editar analisis cuantitativo
    '47-Evaluar analisis cuantitativo manualmente
    '48-Ver detalle analisis cuantitativo

    '***  ANALISIS DEL LOCAL ***
    '26-Adjuntar foto del local
    '27-Administrar comentarios analisis del local
    '28-Editar analisis del local
    '29-Eliminar foto del local 
    '30-Eliminar foto del local propias
    '31-Establecer foto por defecto
    '32-Evaluar analisis del local manualmente
    '33-Ver detalle analisis del local

    '***  ANALISIS DEL ENTORNO ***
    '34-Administrar comentarios analisis del entorno
    '35-Agregar centros de afinidad
    '36-Consultar centros de afinidad
    '37-Dar de alta centros de afinidad
    '38-Dar de baja centros de afinidad 
    '39-Dar de baja centros de afinidad propios
    '40-Editar analisis del entorno
    '41-Evaluar analisis del entorno manualmente
    '42-Quitar centros de afinidad 
    '43-Quitar centros de afinidad propios
    '44-Ver detalle analisis del entorno

    '***  ANALISIS DE LA COMPETENCIA ***
    '49-Administrar comentarios analisis de la competencia
    '50-Agregar competidores
    '51-Consultar competidores
    '52-Dar de alta competidores
    '53-Dar de baja competidores
    '54-Dar de baja competidores propios
    '55-Editar analisis de la competencia
    '56-Evaluar analisis de la competencia
    '57-Quitar competidores
    '58-Quitar competidores propios
    '59-Ver detalle analisis de la competencia

    Public Shared Function getFuncionesRolStr(ByVal rol As Integer, ByRef msgErr As String) As String
        Dim resultado As New StringBuilder("")
        Dim segDAL As New SeguridadDAL
        Dim lage As New ArrayList

        lage = segDAL.getFuncionesRol(rol, msgErr)
        For i = 0 To lage.Count - 1
            resultado.Append(lage(i)).Append("-")
        Next
        Return resultado.ToString
    End Function

    Public Shared Function validarfuncion(ByVal user As System.Security.Principal.IPrincipal, ByVal funcion As Integer) As Boolean
        Dim msgErr As String = ""
        'RL: la lista de funciones debe terminar en "-" para que no falle el split
        'ADMINISTRADOR(todo)
        Dim rol_Administrador As String = "2-8-1-11-5-15-7-21-20-66-67-68-69-70-71-72-"
        rol_Administrador = getFuncionesRolStr(1, msgErr)
        'AGENCIA
        Dim rol_Agencia As String = "9-2-6-16-"
        rol_Agencia = getFuncionesRolStr(2, msgErr)
        'MESA ENTRADA
        Dim rol_Mesa As String = "8-1-11-5-15-7-20-72-"
        rol_Mesa = getFuncionesRolStr(7, msgErr)
        'EVALUADOR
        Dim rol_Evaluador As String = "8-15-21-20-66-67-68-69-70-71-72-"
        rol_Evaluador = getFuncionesRolStr(4, msgErr)
        'EXPERTO
        Dim rol_Experto As String = "8-15-21-20-66-67-68-69-70-71-72-"
        rol_Experto = getFuncionesRolStr(5, msgErr)
        'ASESOR 
        Dim rol_Asistente As String = "8-15-20-"
        rol_Asistente = getFuncionesRolStr(3, msgErr)
        'INSPECTOR
        Dim rol_Inspector As String = "8-15-20-"
        rol_Inspector = getFuncionesRolStr(6, msgErr)
        'CONSULTA
        Dim rol_Consulta As String = "8-15-20-"
        rol_Consulta = getFuncionesRolStr(8, msgErr)

        Dim habilitado As Boolean
        Dim funciones() As String
        Dim perfil As New StringBuilder("")
        Dim j As Integer
        Dim cant As Integer

        habilitado = False

        ' Primero "UNIR" las funciones de todos los roles del usuario
        If user.IsInRole("Agencia") Then
            perfil.Append(rol_Agencia)
        End If
        If user.IsInRole("Evaluador") Then
            perfil.Append(rol_Evaluador)
        End If
        If user.IsInRole("Inspector") Then
            perfil.Append(rol_Inspector)
        End If
        If user.IsInRole("Mesa de Ayuda") Then
            perfil.Append(rol_Mesa)
        End If
        If user.IsInRole("Experto") Then
            perfil.Append(rol_Experto)
        End If
        If user.IsInRole("Asistente Comercial") Then
            perfil.Append(rol_Asistente)
        End If
        If user.IsInRole("Administrador") Then
            perfil.Append(rol_Administrador)
        End If
        If user.IsInRole("Consulta") Then
            perfil.Append(rol_Consulta)
        End If

        ' Luego verificar si la funcion a evaluar esta en la lista
        If perfil.Length > 0 Then  ' Tengo funciones para analizar
            funciones = Split(perfil.ToString, "-")
            cant = funciones.Length - 1
            For j = 0 To cant - 1
                If funciones(j) = funcion Then
                    habilitado = True
                    Exit For
                End If
            Next
        End If
        Return habilitado
    End Function

    Public Shared Function HabilitaEdicionSolicitudNoNueva(ByVal user As System.Security.Principal.IPrincipal) As Boolean
        Dim resultado As Boolean = False
        ' Roles que pueden modificar datos de entorno de la solicitud una vez confirmada:
        '       Administrador, Evaluador, Experto PAC y Asistente Comercial
        If user.IsInRole("Evaluador") Or user.IsInRole("Experto") Or user.IsInRole("Asistente Comercial") Or user.IsInRole("Administrador") Then
            If validarfuncion(user, 11) Then ' 11 - Editar solicitud - todas
                resultado = True
            End If
        End If
        Return resultado
    End Function

    Public Shared Function HabilitaEdicionSolicitudNueva(ByVal user As System.Security.Principal.IPrincipal, ByVal Legajo As Long) As Boolean
        Dim resultado As Boolean = False
        ' Roles que pueden modificar datos de la solicitud antes de ser confirmada:
        '       Agencia: solo si el usuario pertenece a la agencia de la solicitud y tiene asignado el permiso 12
        '       Otros roles: si tienen asignado el permiso 11
        If user.IsInRole("Agencia") Then
            If validarfuncion(user, 12) And UsuarioEnAgencia(user, Legajo) Then ' 11 - Editar solicitud - propias
                resultado = True
            End If
        Else
            If validarfuncion(user, 11) Then ' 11 - Editar solicitud - todas
                resultado = True
            End If
        End If
        Return resultado
    End Function

    Public Shared Function HabilitaAltaSolicitud(ByVal user As System.Security.Principal.IPrincipal, ByVal Legajo As Long) As Boolean
        Dim resultado As Boolean = False
        ' Roles que pueden modificar datos de la solicitud antes de ser confirmada:
        '       Agencia: solo si el usuario pertenece a la agencia de la solicitud y tiene asignado el permiso 12
        '       Otros roles: si tienen asignado el permiso 11
        If user.IsInRole("Agencia") Then
            If validarfuncion(user, 2) And UsuarioEnAgencia(user, Legajo) Then ' 11 - Editar solicitud - propias
                resultado = True
            End If
        Else
            If validarfuncion(user, 1) Then ' 11 - Editar solicitud - todas
                resultado = True
            End If
        End If
        Return resultado
    End Function

    Public Shared Function UsuarioEnAgencia(ByVal user As System.Security.Principal.IPrincipal, ByVal Legajo As Long) As Boolean
        Dim resultado As Boolean = False
        Dim segDAL As New SeguridadDAL
        Dim lage As New ArrayList
        Dim msgErr As String = ""
        lage = segDAL.getAgenciasDelUsuario(user.Identity.Name, msgErr)
        For i = 0 To lage.Count - 1
            If lage(i).ToString.Trim = Legajo.ToString.Trim Then
                resultado = True
            End If
        Next
        Return resultado
    End Function

    '***** FUNCIONES PARA HABILITAR BOTONES EN CONSULTA
    Public Shared Function HabilitarDetalleSolicitud(ByVal user As System.Security.Principal.IPrincipal, ByVal Legajo As Long) As Boolean
        Dim resultado As Boolean = False
        ' 15: Ver detalle todas, 
        ' 16: Ver detalle propias
        resultado = validarfuncion(user, 15) Or (validarfuncion(user, 16) And UsuarioEnAgencia(user, Legajo))
        Return resultado
    End Function

    Public Shared Function HabilitarInforme(ByVal user As System.Security.Principal.IPrincipal) As Boolean
        Dim resultado As Boolean = False
        '66-Agregar informe adjunto
        '67-Agregar informe automatico
        '68-Eliminar informe adjunto
        '69-Eliminar informe automatico
        '70-Obtener copia de informe
        '71-Ver informe
        resultado = validarfuncion(user, 71)
        Return resultado
    End Function

    Public Shared Function HabilitarConsultarEvaluacion(ByVal user As System.Security.Principal.IPrincipal, Optional ByVal Legajo As Long = -1) As Boolean
        Dim resultado As Boolean = False
        ' 20: Consultar Evaluacion 
        resultado = validarfuncion(user, 20)
        Return resultado
    End Function

    Public Shared Function HabilitarConfirmarSolicitud(ByVal user As System.Security.Principal.IPrincipal, Optional ByVal Legajo As Long = -1) As Boolean
        Dim resultado As Boolean = False
        ' 07:Confirmar solicitud
        resultado = validarfuncion(user, 7)
        Return resultado
    End Function

    Public Shared Function HabilitarCancelarSolicitud(ByVal user As System.Security.Principal.IPrincipal, ByVal Legajo As Long) As Boolean
        Dim resultado As Boolean = False
        ' 06:Cancelar solicitud propias
        ' 05:Cancelar solicitud todas
        resultado = validarfuncion(user, 5) Or (validarfuncion(user, 6) And UsuarioEnAgencia(user, Legajo))
        Return resultado
    End Function
End Class
