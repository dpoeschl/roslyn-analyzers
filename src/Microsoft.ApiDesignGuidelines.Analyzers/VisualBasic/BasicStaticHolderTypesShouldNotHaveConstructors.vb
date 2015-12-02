' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Diagnostics
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace Microsoft.ApiDesignGuidelines.Analyzers   
    ''' <summary>
    ''' CA1053: Static holder types should not have constructors
    ''' </summary>
    <DiagnosticAnalyzer(LanguageNames.VisualBasic)>
    Public NotInheritable Class BasicStaticHolderTypesShouldNotHaveConstructorsAnalyzer
        Inherits StaticHolderTypesShouldNotHaveConstructorsAnalyzer

    End Class
End Namespace