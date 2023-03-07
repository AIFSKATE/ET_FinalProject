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
    unsafe class Cinemachine_CinemachineComposer_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Cinemachine.CinemachineComposer);

            field = type.GetField("m_DeadZoneHeight", flag);
            app.RegisterCLRFieldGetter(field, get_m_DeadZoneHeight_0);
            app.RegisterCLRFieldSetter(field, set_m_DeadZoneHeight_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_DeadZoneHeight_0, AssignFromStack_m_DeadZoneHeight_0);
            field = type.GetField("m_DeadZoneWidth", flag);
            app.RegisterCLRFieldGetter(field, get_m_DeadZoneWidth_1);
            app.RegisterCLRFieldSetter(field, set_m_DeadZoneWidth_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_DeadZoneWidth_1, AssignFromStack_m_DeadZoneWidth_1);


        }



        static object get_m_DeadZoneHeight_0(ref object o)
        {
            return ((Cinemachine.CinemachineComposer)o).m_DeadZoneHeight;
        }

        static StackObject* CopyToStack_m_DeadZoneHeight_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineComposer)o).m_DeadZoneHeight;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_DeadZoneHeight_0(ref object o, object v)
        {
            ((Cinemachine.CinemachineComposer)o).m_DeadZoneHeight = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_DeadZoneHeight_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_DeadZoneHeight = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineComposer)o).m_DeadZoneHeight = @m_DeadZoneHeight;
            return ptr_of_this_method;
        }

        static object get_m_DeadZoneWidth_1(ref object o)
        {
            return ((Cinemachine.CinemachineComposer)o).m_DeadZoneWidth;
        }

        static StackObject* CopyToStack_m_DeadZoneWidth_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineComposer)o).m_DeadZoneWidth;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_DeadZoneWidth_1(ref object o, object v)
        {
            ((Cinemachine.CinemachineComposer)o).m_DeadZoneWidth = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_DeadZoneWidth_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_DeadZoneWidth = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineComposer)o).m_DeadZoneWidth = @m_DeadZoneWidth;
            return ptr_of_this_method;
        }



    }
}
