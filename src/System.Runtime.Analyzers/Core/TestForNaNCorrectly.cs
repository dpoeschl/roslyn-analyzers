// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Analyzer.Utilities;
using Microsoft.CodeAnalysis.Semantics;

namespace System.Runtime.Analyzers
{
    /// <summary>
    /// CA2242: Test for NaN correctly
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
    public class TestForNaNCorrectlyAnalyzer : DiagnosticAnalyzer
    {
        internal const string RuleId = "CA2242";

        private static readonly LocalizableString s_localizableTitle = new LocalizableResourceString(nameof(SystemRuntimeAnalyzersResources.TestForNaNCorrectlyTitle), SystemRuntimeAnalyzersResources.ResourceManager, typeof(SystemRuntimeAnalyzersResources));
        
        private static readonly LocalizableString s_localizableMessage = new LocalizableResourceString(nameof(SystemRuntimeAnalyzersResources.TestForNaNCorrectlyMessage), SystemRuntimeAnalyzersResources.ResourceManager, typeof(SystemRuntimeAnalyzersResources));
        private static readonly LocalizableString s_localizableDescription = new LocalizableResourceString(nameof(SystemRuntimeAnalyzersResources.TestForNaNCorrectlyDescription), SystemRuntimeAnalyzersResources.ResourceManager, typeof(SystemRuntimeAnalyzersResources));
        
        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(RuleId,
                                                                             s_localizableTitle,
                                                                             s_localizableMessage,
                                                                             DiagnosticCategory.Usage,
                                                                             DiagnosticSeverity.Warning,
                                                                             isEnabledByDefault: true,
                                                                             description: s_localizableDescription,
                                                                             helpLinkUri: null,     // TODO: add MSDN url
                                                                             customTags: WellKnownDiagnosticTags.Telemetry);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext analysisContext)
        {
            analysisContext.RegisterSymbolAction(
                context => { int x = 7; }, SymbolKind.NamedType); // BP Hit

            analysisContext.RegisterOperationAction(
                context => { int y = 5; }, OperationKind.LiteralExpression); // BP Not hit


            analysisContext.RegisterOperationBlockStartAction(
                context => { var t = 4; }); // BP Hit

            analysisContext.RegisterCompilationStartAction(
                context =>
                {
                    analysisContext.RegisterOperationAction( // BP Hit
                        context2 =>
                        {
                            var binOp = context2.Operation as IBinaryOperatorExpression; // BP Not hit
                            if (binOp.BinaryKind == BinaryOperationKind.FloatingEquals)
                            {
                            }
                        },
                        OperationKind.BinaryOperatorExpression);
                });

            analysisContext.RegisterOperationAction(
                context =>
                {
                    var binOp = context.Operation as IBinaryOperatorExpression;  // BP Not hit
                    if (binOp.BinaryKind == BinaryOperationKind.FloatingEquals)
                    {
                    }
                },
                OperationKind.BinaryOperatorExpression);
        }
    }
}