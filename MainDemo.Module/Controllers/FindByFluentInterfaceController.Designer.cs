namespace MainDemo.Module.Controllers {
	partial class FindUsingFluentInterfaceController {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
            this.FindUsingFluentInterfaceAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
			// 
            // FindUsingFluentInterfaceController
			// 
			this.FindUsingFluentInterfaceAction.Caption = "Test Fluent Interface";
            this.FindUsingFluentInterfaceAction.Category = "View";
			this.FindUsingFluentInterfaceAction.Id = "FindByFluentInterfaceAction";
			this.FindUsingFluentInterfaceAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(FindByFluentInterfaceAction_Execute);
			// 
            // FindUsingFluentInterfaceController
			// 
			this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
			this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TargetObjectType = typeof(MainDemo.Module.BusinessObjects.Contact);
		}

		#endregion
		private DevExpress.ExpressApp.Actions.SimpleAction FindUsingFluentInterfaceAction;		
	}
}
