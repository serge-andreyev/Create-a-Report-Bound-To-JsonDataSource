﻿Imports DevExpress.DataAccess.Json
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml.Linq

Namespace Create_a_Report_Bound_to_JsonDataSource
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim report As XtraReport = CreateReport()
            Dim designTool As New ReportDesignTool(report)
            designTool.ShowRibbonDesignerDialog()
        End Sub

        Private Function CreateReport() As XtraReport
            Dim report As New XtraReport()
            Dim DetailBand As New DetailBand()
            DetailBand.HeightF = 50

            Dim XRLabel As New XRLabel()
            XRLabel.WidthF = 300
            XRLabel.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", "[CompanyName]"))

            DetailBand.Controls.Add(XRLabel)
            report.Bands.Add(DetailBand)



            report.DataSource = CreateDataSourceFromWeb()
            'report.DataSource = CreateDataSourceFromFile();
            'report.DataSource = CreateDataSourceFromText();
            'report.DataMember = "Customers";
            Return report
        End Function
        Private Function CreateDataSourceFromWeb() As JsonDataSource
            Dim jsonDataSource = New JsonDataSource()
            ' Specify a Web Service Endpoint URI with JSON content
            Dim uri = New Uri("http://northwind.servicestack.net/customers.json")
            jsonDataSource.JsonSource = New UriJsonSource(uri)
            Return jsonDataSource
        End Function

        Private Function CreateDataSourceFromFile() As JsonDataSource
            Dim jsonDataSource = New JsonDataSource()
            'Specify the a JSON file's name
            Dim fileUri As New Uri("file:///../../../../customers.txt")
            jsonDataSource.JsonSource = New UriJsonSource(fileUri)
            Return jsonDataSource
        End Function

        Private Function CreateDataSourceFromText() As JsonDataSource

            Dim jsonDataSource = New JsonDataSource()

            'Specify a string with JSON content
            Dim json As String = "{""Customers"":[{""Id"":""ALFKI"",""CompanyName"":""Alfreds Futterkiste"",""ContactName"":""Maria Anders"",""ContactTitle"":""Sales Representative"",""Address"":""Obere Str. 57"",""City"":""Berlin"",""PostalCode"":""12209"",""Country"":""Germany"",""Phone"":""030-0074321"",""Fax"":""030-0076545""}],""ResponseStatus"":{}}"

            ' Specify the object that retrieves JSON data
            jsonDataSource.JsonSource = New CustomJsonSource(json)
            Return jsonDataSource
        End Function
    End Class
End Namespace