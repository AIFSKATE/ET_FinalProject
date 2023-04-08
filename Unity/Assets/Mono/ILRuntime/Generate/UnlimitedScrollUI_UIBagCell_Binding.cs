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
    unsafe class UnlimitedScrollUI_UIBagCell_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(UnlimitedScrollUI.UIBagCell);
            args = new Type[]{typeof(System.Action<UnlimitedScrollUI.ScrollerPanelSide>)};
            method = type.GetMethod("add_on_BecomeInvisible", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_on_BecomeInvisible_0);
            args = new Type[]{typeof(System.Action<UnlimitedScrollUI.ScrollerPanelSide>)};
            method = type.GetMethod("add_on_BecomeVisible", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_on_BecomeVisible_1);


        }


        static StackObject* add_on_BecomeInvisible_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnlimitedScrollUI.ScrollerPanelSide> @value = (System.Action<UnlimitedScrollUI.ScrollerPanelSide>)typeof(System.Action<UnlimitedScrollUI.ScrollerPanelSide>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnlimitedScrollUI.UIBagCell instance_of_this_method = (UnlimitedScrollUI.UIBagCell)typeof(UnlimitedScrollUI.UIBagCell).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.on_BecomeInvisible += value;

            return __ret;
        }

        static StackObject* add_on_BecomeVisible_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnlimitedScrollUI.ScrollerPanelSide> @value = (System.Action<UnlimitedScrollUI.ScrollerPanelSide>)typeof(System.Action<UnlimitedScrollUI.ScrollerPanelSide>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnlimitedScrollUI.UIBagCell instance_of_this_method = (UnlimitedScrollUI.UIBagCell)typeof(UnlimitedScrollUI.UIBagCell).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.on_BecomeVisible += value;

            return __ret;
        }



    }
}
