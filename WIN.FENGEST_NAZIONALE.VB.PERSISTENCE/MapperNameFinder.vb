Public Class MapperNameFinder

    Public Shared Function GetRealMapperName(ByVal MapperName As String) As String
        'Select Case MapperName
        'Case "MapperItemIncassoQuotaAssociativa"
        '    Return "MapperItemDocumentoContabile"
        'Case "MapperItemIncassoQuotaPrevisionale"
        '    Return "MapperItemDocumentoContabile"
        'Case "MapperItemPagamentoReferenti"
        '    Return "MapperItemDocumentoContabile"
        'Case "MapperItemIncassoQuoteInps"
        '    Return "MapperItemDocumentoContabile"
        'Case "MapperItemIncassoQuoteVertenza"
        '    Return "MapperItemDocumentoContabile"
        'Case Else
        Return MapperName
        'End Select
    End Function
End Class
