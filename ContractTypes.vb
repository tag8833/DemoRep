Imports Microsoft.ApplicationBlocks.Data
Imports Common
Imports System.Data.SqlClient

Public Class ContractTypes

    '******************************************************
    ' Private Data To Match the Table Definition
    '******************************************************

    Private miContractTypeID As Integer
    Private msContractType As String

    '******************************************************
    ' Properties
    '******************************************************

    Property ContractTypeID() As Integer
        Get
            Return miContractTypeID
        End Get
        Set(ByVal Value As Integer)
            miContractTypeID = Value
        End Set
    End Property

    Property ContractType() As String
        Get
            Return msContractType
        End Get
        Set(ByVal Value As String)
            msContractType = Value
        End Set
    End Property


    '******************************************************
    ' Methods
    '******************************************************

    Public Function GetDataset(ByVal sStartsWith As String, ByRef dt As DataTable) As sysResult

        Dim myResult As New sysResult
        Try
            Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypes", New SqlParameter("@StartsWith", sStartsWith))
            If Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
                myResult.Successful = True
            End If
        Catch ex As Exception
            With myResult
                .Successful = False
                .Message = ex.Message
            End With
        End Try
        Return myResult

    End Function


    Public Function GetList(ByRef dt As DataTable) As sysResult

        Dim myResult As New sysResult
        Try
            Dim ds As DataSet = SqlHelper.ExecuteDataset(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypesList")
            If Not ds.Tables(0) Is Nothing Then
                dt = ds.Tables(0)
                myResult.Successful = True
            End If
        Catch ex As Exception
            With myResult
                .Successful = False
                .Message = ex.Message
            End With
        End Try
        Return myResult

    End Function


    Public Function AddRecord(ByVal myObject As ContractTypes) As sysResult

        Dim myResult As New sysResult
        'returns the RowID of the added record
        Try
            With myObject
                myResult.IntegerID = SqlHelper.ExecuteScalar(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypesAdd", New SqlParameter("@ContractType", .ContractType))
                myResult.Successful = True
            End With
        Catch ex As Exception
            With myResult
                .Successful = False
                .Message = ex.Message
                .StringID = ""
                .IntegerID = 0
                .DoubleID = 0
            End With
        End Try
        Return myResult

    End Function


    Public Function Find(ByVal iContractTypeID As Integer) As sysResult

        Dim myResult As New sysResult
        Dim drTemp As SqlDataReader

        InitToBlank()

        Try
            drTemp = SqlHelper.ExecuteReader(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypesFind", New SqlParameter("@ContractTypeID", iContractTypeID))
            Try
                With drTemp
                    If .Read() Then

                        If Not (.IsDBNull(0)) Then miContractTypeID = .GetInt32(0)
                        If Not (.IsDBNull(1)) Then msContractType = .GetString(1)

                        myResult.Successful = True
                        myResult.Found = True
                    Else
                        With myResult
                            .Successful = True
                            .Found = False
                            .Message = "Record Not Found"
                        End With
                    End If
                End With
            Catch ex As Exception
                With myResult
                    .Successful = False
                    .Found = False
                    .Message = ex.Message
                End With
            Finally
                drTemp.Close()
            End Try
        Catch ex As Exception
            With myResult
                .Successful = False
                .Found = False
                .Message = ex.Message
            End With
        End Try

        Return myResult

    End Function


    Public Sub InitToBlank()

        miContractTypeID = Nothing
        msContractType = Nothing

    End Sub


    Public Function EditRecord(ByVal myObject As ContractTypes) As sysResult

        Dim myResult As New sysResult
        Try
            With myObject
                SqlHelper.ExecuteScalar(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypesEdit", New SqlParameter("@ContractTypeID", .ContractTypeID), New SqlParameter("@ContractType", .ContractType))
                myResult.Successful = True
            End With
        Catch ex As Exception
            With myResult
                .Successful = False
                .Message = ex.Message
                .StringID = ""
                .IntegerID = 0
                .DoubleID = 0
            End With
        End Try
        Return myResult

    End Function


    Public Function DeleteRecord(ByVal iContractTypeID As Integer) As sysResult

        Dim myResult As New sysResult
        Try
            SqlHelper.ExecuteScalar(ConfigInfo.ConnectString, CommandType.StoredProcedure, "spContractTypesDelete", New SqlParameter("@ContractTypeID", iContractTypeID))
            myResult.Successful = True
        Catch ex As Exception
            With myResult
                .Successful = False
                .Message = ex.Message
                .StringID = ""
                .IntegerID = 0
                .DoubleID = 0
            End With
        End Try
        Return myResult

    End Function

End Class
