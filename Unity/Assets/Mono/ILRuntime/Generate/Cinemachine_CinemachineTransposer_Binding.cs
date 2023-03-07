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
    unsafe class Cinemachine_CinemachineTransposer_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Cinemachine.CinemachineTransposer);

            field = type.GetField("m_FollowOffset", flag);
            app.RegisterCLRFieldGetter(field, get_m_FollowOffset_0);
            app.RegisterCLRFieldSetter(field, set_m_FollowOffset_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_FollowOffset_0, AssignFromStack_m_FollowOffset_0);
            field = type.GetField("m_BindingMode", flag);
            app.RegisterCLRFieldGetter(field, get_m_BindingMode_1);
            app.RegisterCLRFieldSetter(field, set_m_BindingMode_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_BindingMode_1, AssignFromStack_m_BindingMode_1);
            field = type.GetField("m_XDamping", flag);
            app.RegisterCLRFieldGetter(field, get_m_XDamping_2);
            app.RegisterCLRFieldSetter(field, set_m_XDamping_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_XDamping_2, AssignFromStack_m_XDamping_2);
            field = type.GetField("m_YDamping", flag);
            app.RegisterCLRFieldGetter(field, get_m_YDamping_3);
            app.RegisterCLRFieldSetter(field, set_m_YDamping_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_YDamping_3, AssignFromStack_m_YDamping_3);
            field = type.GetField("m_ZDamping", flag);
            app.RegisterCLRFieldGetter(field, get_m_ZDamping_4);
            app.RegisterCLRFieldSetter(field, set_m_ZDamping_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_ZDamping_4, AssignFromStack_m_ZDamping_4);
            field = type.GetField("m_YawDamping", flag);
            app.RegisterCLRFieldGetter(field, get_m_YawDamping_5);
            app.RegisterCLRFieldSetter(field, set_m_YawDamping_5);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_YawDamping_5, AssignFromStack_m_YawDamping_5);


        }



        static object get_m_FollowOffset_0(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_FollowOffset;
        }

        static StackObject* CopyToStack_m_FollowOffset_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_FollowOffset;
            if (ILRuntime.Runtime.Generated.CLRBindings.s_UnityEngine_Vector3_Binding_Binder != null) {
                ILRuntime.Runtime.Generated.CLRBindings.s_UnityEngine_Vector3_Binding_Binder.PushValue(ref result_of_this_method, __intp, __ret, __mStack);
                return __ret + 1;
            } else {
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
            }
        }

        static void set_m_FollowOffset_0(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_FollowOffset = (UnityEngine.Vector3)v;
        }

        static StackObject* AssignFromStack_m_FollowOffset_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Vector3 @m_FollowOffset = new UnityEngine.Vector3();
            if (ILRuntime.Runtime.Generated.CLRBindings.s_UnityEngine_Vector3_Binding_Binder != null) {
                ILRuntime.Runtime.Generated.CLRBindings.s_UnityEngine_Vector3_Binding_Binder.ParseValue(ref @m_FollowOffset, __intp, ptr_of_this_method, __mStack, true);
            } else {
                @m_FollowOffset = (UnityEngine.Vector3)typeof(UnityEngine.Vector3).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);
            }
            ((Cinemachine.CinemachineTransposer)o).m_FollowOffset = @m_FollowOffset;
            return ptr_of_this_method;
        }

        static object get_m_BindingMode_1(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_BindingMode;
        }

        static StackObject* CopyToStack_m_BindingMode_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_BindingMode;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_BindingMode_1(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_BindingMode = (Cinemachine.CinemachineTransposer.BindingMode)v;
        }

        static StackObject* AssignFromStack_m_BindingMode_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            Cinemachine.CinemachineTransposer.BindingMode @m_BindingMode = (Cinemachine.CinemachineTransposer.BindingMode)typeof(Cinemachine.CinemachineTransposer.BindingMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)20);
            ((Cinemachine.CinemachineTransposer)o).m_BindingMode = @m_BindingMode;
            return ptr_of_this_method;
        }

        static object get_m_XDamping_2(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_XDamping;
        }

        static StackObject* CopyToStack_m_XDamping_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_XDamping;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_XDamping_2(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_XDamping = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_XDamping_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_XDamping = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineTransposer)o).m_XDamping = @m_XDamping;
            return ptr_of_this_method;
        }

        static object get_m_YDamping_3(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_YDamping;
        }

        static StackObject* CopyToStack_m_YDamping_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_YDamping;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_YDamping_3(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_YDamping = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_YDamping_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_YDamping = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineTransposer)o).m_YDamping = @m_YDamping;
            return ptr_of_this_method;
        }

        static object get_m_ZDamping_4(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_ZDamping;
        }

        static StackObject* CopyToStack_m_ZDamping_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_ZDamping;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_ZDamping_4(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_ZDamping = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_ZDamping_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_ZDamping = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineTransposer)o).m_ZDamping = @m_ZDamping;
            return ptr_of_this_method;
        }

        static object get_m_YawDamping_5(ref object o)
        {
            return ((Cinemachine.CinemachineTransposer)o).m_YawDamping;
        }

        static StackObject* CopyToStack_m_YawDamping_5(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineTransposer)o).m_YawDamping;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_YawDamping_5(ref object o, object v)
        {
            ((Cinemachine.CinemachineTransposer)o).m_YawDamping = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_YawDamping_5(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_YawDamping = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineTransposer)o).m_YawDamping = @m_YawDamping;
            return ptr_of_this_method;
        }



    }
}
