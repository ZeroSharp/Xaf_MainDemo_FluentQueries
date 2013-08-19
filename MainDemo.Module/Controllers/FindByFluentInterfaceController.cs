using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using MainDemo.Module.BusinessObjects;
using MainDemo.Module.Queries;

namespace MainDemo.Module.Controllers {
    public partial class FindUsingFluentInterfaceController : ViewController {
        public FindUsingFluentInterfaceController()
            : base() {
            InitializeComponent();
            RegisterActions(components);
        }
        private void FindByFluentInterfaceAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            IObjectSpace objectSpace = Application.CreateObjectSpace();

            var customer = objectSpace
                .Query()
                .InTransaction
                    .Contacts
                        .ByPosition("Developer")
                            .ThatHave
                                .Resume()
                            .And
                                .TasksInProgress()
                            .And
                                .TasksWith(Priority.High)
                .FirstOrDefault();

            if(customer != null) {
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, customer);
            }
        }
    }
}
