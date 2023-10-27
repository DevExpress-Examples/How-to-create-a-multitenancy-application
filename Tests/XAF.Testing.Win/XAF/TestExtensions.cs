﻿using System.Reactive.Linq;
using System.Reactive.Subjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.Map.Kml.Model;
using XAF.Testing.RX;
using XAF.Testing.XAF;
using Point = System.Drawing.Point;

namespace XAF.Testing.Win.XAF{
    public static class TestExtensions{
        public static IObservable<T> StartWinTest<T>(this WinApplication application, IObservable<T> test,string user,LogContext logContext=default) 
            => application.Start( test.Log(logContext)
                , new WindowsFormsSynchronizationContext(),user);

        private static IObservable<T> Start<T>(this WinApplication application, IObservable<T> test, WindowsFormsSynchronizationContext context,string user =null) 
            => context.Observe().Do(SynchronizationContext.SetSynchronizationContext)
                .SelectMany(_ => application.Start(Tracing.WhenError().ThrowTestException().DoOnError(_ => application.Terminate(context)).To<T>()
                    .Merge(application.WhenLoggedOn(user).Take(1).IgnoreElements().To<T>()
                        .Merge(test.DoOnComplete(() => application.Terminate(context)))
                        .Merge(application.ThrowWhenHandledExceptions().To<T>()).LogError())));

        private static void Terminate(this XafApplication application, SynchronizationContext context) 
            => context.Post(_ => application.Exit(),null);

        public static IObservable<Form> MoveToInactiveMonitor(this IObservable<Form> source) 
            => source.Do( form => form.Handle.UseInactiveMonitorBounds(bounds => {
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(bounds.Left, bounds.Top);    
            }));

        public static void ChangeStartupState(this WinApplication application,FormWindowState windowState,bool moveToInactiveMonitor=true) 
            => application.WhenFrameCreated(TemplateContext.ApplicationWindow)
                .TemplateChanged().Select(frame => frame.Template)
                .Cast<Form>()
                .If(_ => moveToInactiveMonitor,form => form.Observe().MoveToInactiveMonitor(),form => form.Observe())
                .Do(form => form.WindowState = windowState).Take(1)
                .Subscribe();
        
        public static IObservable<T> Start<T>(this WinApplication application, IObservable<T> test){
            var exitSignal = new Subject<Unit>();
            return test.Merge(application.Defer(() => application.Observe().Do(winApplication => winApplication.Start()).Do(_ => exitSignal.OnNext())
                    .Select(winApplication => winApplication)
                    .Finally(() => { })
                    .Catch<XafApplication, Exception>(exception => {
                        DevExpress.Persistent.Base.Tracing.Tracer.LogError(exception);
                        return Observable.Empty<XafApplication>();
                    }).IgnoreElements()
                    .To<T>()))
                .TakeUntil(exitSignal);
        }
    }
}