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
    unsafe class ET_DelegateMonoBehaviour_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.DelegateMonoBehaviour);
            args = new Type[]{};
            method = type.GetMethod("Clear", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Clear_0);
            args = new Type[]{typeof(System.Action<UnityEngine.Collider>)};
            method = type.GetMethod("add_on_TriggerEnter", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_on_TriggerEnter_1);

            field = type.GetField("BelongToUnitId", flag);
            app.RegisterCLRFieldGetter(field, get_BelongToUnitId_0);
            app.RegisterCLRFieldSetter(field, set_BelongToUnitId_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_BelongToUnitId_0, AssignFromStack_BelongToUnitId_0);


        }


        static StackObject* Clear_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ET.DelegateMonoBehaviour instance_of_this_method = (ET.DelegateMonoBehaviour)typeof(ET.DelegateMonoBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Clear();

            return __ret;
        }

        static StackObject* add_on_TriggerEnter_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.Collider> @value = (System.Action<UnityEngine.Collider>)typeof(System.Action<UnityEngine.Collider>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            ET.DelegateMonoBehaviour instance_of_this_method = (ET.DelegateMonoBehaviour)typeof(ET.DelegateMonoBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.on_TriggerEnter += value;

            return __ret;
        }


        static object get_BelongToUnitId_0(ref object o)
        {
            return ((ET.DelegateMonoBehaviour)o).BelongToUnitId;
        }

        static StackObject* CopyToStack_BelongToUnitId_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DelegateMonoBehaviour)o).BelongToUnitId;
            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_BelongToUnitId_0(ref object o, object v)
        {
            ((ET.DelegateMonoBehaviour)o).BelongToUnitId = (System.Int64)v;
        }

        static StackObject* AssignFromStack_BelongToUnitId_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int64 @BelongToUnitId = *(long*)&ptr_of_this_method->Value;
            ((ET.DelegateMonoBehaviour)o).BelongToUnitId = @BelongToUnitId;
            return ptr_of_this_method;
        }



    }
}
