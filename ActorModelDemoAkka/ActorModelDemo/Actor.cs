using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//namespace ActorModelDemo.Actors
//{
//    /// <summary>
//    /// http://www.jayway.com/2013/11/15/an-actor-model-implementation-in-c-using-tpl-dataflow/
//    /// </summary>
//    internal abstract class Actor
//    {
//        private readonly ActionBlock<Message> p_Action;

//        public Actor()
//        {
//            p_Action = new ActionBlock<Message>(message =>
//            {
//                dynamic self = this;
//                dynamic mess = message;
//                self.Handle(mess);
//            });
//        }

//        public void p_Send(Message message)
//        {
//            p_Action.Post(message);
//        }
//    }
//}
