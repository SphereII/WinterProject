using System;
using SDX.Compiler;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;
using System.Collections.Generic;
using SDX.Core;
public class WinterMod : IPatcherMod
{
  
    // Debug Logging
    private bool DebugLog = true;

    public bool Patch(ModuleDefinition module)
    {
        Log("=== Winter Mod Patcher ===");
        AllowSnowFilledAreas(module);

        return true;
    }

 
    // Set the filter on the search to be 2 or 3 characters
    private void AllowSnowFilledAreas( ModuleDefinition module )
    {
        Log("Searching for Prefab Class");
        var myClass = module.Types.First(d => d.Name == "Prefab");
        Log("Searching for PrefabChunk Sub Class");
        var myNestedClass = myClass.NestedTypes.First(d => d.Name == "PrefabChunk");
        Log("Searching for GetBlock Method");
        var myMethod = myNestedClass.Methods.First(d => d.Name == "GetBlock");

        Log("Searching for Block Class");
        var myBlock = module.Types.First(d => d.Name == "Block");

        Log("Searching for GetBlock Method of the Block Class");
        var myGetBlock = myBlock.Methods.First(d => d.Name == "GetBlockValue");
        
        var instructions = myMethod.Body.Instructions;
        var pro = myMethod.Body.GetILProcessor();
        foreach (var i in instructions.Reverse())
        {
            // We want to replace the Air block and point to our snowFill block, which will have a custom class
            // and allow us to fill up spaces with a block, instead of air.
            if (i.OpCode == OpCodes.Ldsfld )
            {
                i.OpCode = OpCodes.Ldstr;
                i.Operand = "snowFill";
                pro.InsertAfter(i, Instruction.Create(OpCodes.Call, myGetBlock));
                break;
            }

        }

  

    }


    // Called after the patching process and after scripts are compiled.
    // Used to link references between both assemblies
    // Return true if successful
    public bool Link(ModuleDefinition gameModule, ModuleDefinition modModule)
    {
        return true;
    }

 
    // Helper functions to allow us to access and change variables that are otherwise unavailable.
    private void SetMethodToVirtual(MethodDefinition meth)
    {
        meth.IsVirtual = true;
    }

    private void SetFieldToPublic(FieldDefinition field)
    {
        field.IsFamily = false;
        field.IsPrivate = false;
        field.IsPublic = true;

    }
    private void SetMethodToPublic(MethodDefinition field)
    {
        field.IsFamily = false;
        field.IsPrivate = false;
        field.IsPublic = true;

    }

    private void Log( String strLogMessage )
    {
        if (this.DebugLog == true)
            SDX.Core.Logging.LogInfo( this.GetType().Name.ToString() + ": " + strLogMessage);

    }
}