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
    unsafe class ET_DelegateCollider_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.DelegateCollider);

            field = type.GetField("BelongToUnitId", flag);
            app.RegisterCLRFieldGetter(field, get_BelongToUnitId_0);
            app.RegisterCLRFieldSetter(field, set_BelongToUnitId_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_BelongToUnitId_0, AssignFromStack_BelongToUnitId_0);
            field = type.GetField("on_TriggerEnter", flag);
            app.RegisterCLRFieldGetter(field, get_on_TriggerEnter_1);
            app.RegisterCLRFieldSetter(field, set_on_TriggerEnter_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_on_TriggerEnter_1, AssignFromStack_on_TriggerEnter_1);


        }



        static object get_BelongToUnitId_0(ref object o)
        {
            return ((ET.DelegateCollider)o).BelongToUnitId;
        }

        static StackObject* CopyToStack_BelongToUnitId_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DelegateCollider)o).BelongToUnitId;
            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_BelongToUnitId_0(ref object o, object v)
        {
            ((ET.DelegateCollider)o).BelongToUnitId = (System.Int64)v;
        }

        static StackObject* AssignFromStack_BelongToUnitId_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int64 @BelongToUnitId = *(long*)&ptr_of_this_method->Value;
            ((ET.DelegateCollider)o).BelongToUnitId = @BelongToUnitId;
            return ptr_of_this_method;
        }

        static object get_on_TriggerEnter_1(ref object o)
        {
            return ((ET.DelegateCollider)o).on_TriggerEnter;
        }

        static StackObject* CopyToStack_on_TriggerEnter_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DelegateCollider)o).on_TriggerEnter;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_on_TriggerEnter_1(ref object o, object v)
        {
            ((ET.DelegateCollider)o).on_TriggerEnter = (System.Action<UnityEngine.Collider>)v;
        }

        static StackObject* AssignFromStack_on_TriggerEnter_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action<UnityEngine.Collider> @on_TriggerEnter = (System.Action<UnityEngine.Collider>)typeof(System.Action<UnityEngine.Collider>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((ET.DelegateCollider)o).on_TriggerEnter = @on_TriggerEnter;
            return ptr_of_this_method;
        }



    }
}
