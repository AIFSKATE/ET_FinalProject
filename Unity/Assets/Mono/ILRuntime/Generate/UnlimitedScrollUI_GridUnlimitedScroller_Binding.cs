using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UnlimitedScrollUI_GridUnlimitedScroller_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(UnlimitedScrollUI.GridUnlimitedScroller);
            args = new Type[]{};
            method = type.GetMethod("DestroyAllCells", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, DestroyAllCells_0);
            args = new Type[]{};
            method = type.GetMethod("GenerateAllCells", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GenerateAllCells_1);
            args = new Type[]{typeof(UnityEngine.GameObject), typeof(System.Int32), typeof(System.Action<System.Int32, UnlimitedScrollUI.ICell>)};
            method = type.GetMethod("Generate", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Generate_2);

            field = type.GetField("ETInstantiate", flag);
            app.RegisterCLRFieldGetter(field, get_ETInstantiate_0);
            app.RegisterCLRFieldSetter(field, set_ETInstantiate_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_ETInstantiate_0, AssignFromStack_ETInstantiate_0);


        }


        static StackObject* DestroyAllCells_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnlimitedScrollUI.GridUnlimitedScroller instance_of_this_method = (UnlimitedScrollUI.GridUnlimitedScroller)typeof(UnlimitedScrollUI.GridUnlimitedScroller).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.DestroyAllCells();

            return __ret;
        }

        static StackObject* GenerateAllCells_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnlimitedScrollUI.GridUnlimitedScroller instance_of_this_method = (UnlimitedScrollUI.GridUnlimitedScroller)typeof(UnlimitedScrollUI.GridUnlimitedScroller).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.GenerateAllCells();

            return __ret;
        }

        static StackObject* Generate_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32, UnlimitedScrollUI.ICell> @onGenerate = (System.Action<System.Int32, UnlimitedScrollUI.ICell>)typeof(System.Action<System.Int32, UnlimitedScrollUI.ICell>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @newTotalCount = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityEngine.GameObject @newCell = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            UnlimitedScrollUI.GridUnlimitedScroller instance_of_this_method = (UnlimitedScrollUI.GridUnlimitedScroller)typeof(UnlimitedScrollUI.GridUnlimitedScroller).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Generate(@newCell, @newTotalCount, @onGenerate);

            return __ret;
        }


        static object get_ETInstantiate_0(ref object o)
        {
            return ((UnlimitedScrollUI.GridUnlimitedScroller)o).ETInstantiate;
        }

        static StackObject* CopyToStack_ETInstantiate_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnlimitedScrollUI.GridUnlimitedScroller)o).ETInstantiate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_ETInstantiate_0(ref object o, object v)
        {
            ((UnlimitedScrollUI.GridUnlimitedScroller)o).ETInstantiate = (System.Func<UnityEngine.GameObject, UnityEngine.RectTransform, UnityEngine.GameObject>)v;
        }

        static StackObject* AssignFromStack_ETInstantiate_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Func<UnityEngine.GameObject, UnityEngine.RectTransform, UnityEngine.GameObject> @ETInstantiate = (System.Func<UnityEngine.GameObject, UnityEngine.RectTransform, UnityEngine.GameObject>)typeof(System.Func<UnityEngine.GameObject, UnityEngine.RectTransform, UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((UnlimitedScrollUI.GridUnlimitedScroller)o).ETInstantiate = @ETInstantiate;
            return ptr_of_this_method;
        }



    }
}
