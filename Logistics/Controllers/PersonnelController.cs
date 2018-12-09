using Logistics.Areas.Config.Repositories;
using Logistics.Areas.Inventory.Repositories;
using Logistics.Entities;
using Logistics.Entities.Contact;
using Logistics.Entities.Personnel;
using Logistics.Helpers;
using Logistics.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class PersonnelController : BaseController
    {
        #region Main

        private PersonnelRepository perRepo;
        private AddressRepository addressRepo;
        private EmailRepository emailRepo;
        private PhoneRepository phoneRepo;
        private ExamRepository examRepo;
        private ExamResultRepository resultRepo;
        private LookupRepository lookupRepo;
        private AssetRepository assetRepo;

        public PersonnelController()
        {
            this.perRepo = new PersonnelRepository();
            this.addressRepo = new AddressRepository();
            this.emailRepo = new EmailRepository();
            this.phoneRepo = new PhoneRepository();
            this.resultRepo = new ExamResultRepository();
            this.lookupRepo = new LookupRepository();
        }

        #endregion

        #region Person

        // GET: Personnel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.perRepo.Get(id);
            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Person personnel)
        {
            long oldId = personnel.Id;
            personnel.ModifiedBy = SessionHelper.SatisUser.Id;
            this.perRepo.Save(personnel);

            //MessageHelper message = new MessageHelper(true, "The Person was saved successfully.");

            //if (oldId != personnel.Id)
            //    return RedirectToAction($"Details/{personnel.Id}");
            //else
            //    return RedirectToAction("Index");

            //var returnValue = new
            //{
            //    Message = new MessageHelper(true, "The Person was saved successfully."),
            //    NewId = personnel.Id
            //};
            //return Json(returnValue, JsonRequestBehavior.AllowGet);
            MessageHelper message = new MessageHelper(true, "The Person was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.perRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Person was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Emails

        public ActionResult EmailDetails(long id = 0, long personId = 0)
        {
            EmailAddress email = this.emailRepo.Get(id);
            if (personId != 0)
                email = this.GetPerson<EmailAddress>(personId);

            //var modal = personId == 0 ? this.emailRepo.Get(id) : new EmailAddress { PersonId = personId, Person = this.perRepo.Get(personId) };
            var modal = email;

            ViewBag.ListOfAddressTypes = this.GetAddressTypes(modal.TypeLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult SaveEmail(EmailAddress email)
        {
            email.ModifiedBy = SessionHelper.SatisUser.Id;
            this.emailRepo.Save(email);

            MessageHelper message = new MessageHelper(true, "The Email was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmail(long id)
        {
            this.emailRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Email was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Addresses

        public ActionResult AddressDetails(long id = 0, long personId = 0)
        {
            Address address = this.addressRepo.Get(id);
            if (personId != 0)
                address = this.GetPerson<Address>(personId);

            //var modal = personId == 0 ? this.addressRepo.Get(id) : new Address { PersonId = personId };
            var modal = address;

            ViewBag.ListOfAddressTypes = this.GetAddressTypes(modal.TypeLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult SaveAddress(Address address)
        {
            address.ModifiedBy = SessionHelper.SatisUser.Id;
            this.addressRepo.Save(address);

            MessageHelper message = new MessageHelper(true, "The Address was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAddress(long id)
        {
            this.addressRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Address was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Phones

        public ActionResult PhoneDetails(long id = 0, long personId = 0)
        {
            Phone phone = this.phoneRepo.Get(id);
            if (personId != 0)
                phone = this.GetPerson<Phone>(personId);

            //var modal = personId == 0 ? this.phoneRepo.Get(id) : new Phone { PersonId = personId };
            var modal = phone;

            ViewBag.ListOfAddressTypes = this.GetAddressTypes(modal.TypeLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult SavePhone(Phone phone)
        {
            phone.ModifiedBy = SessionHelper.SatisUser.Id;
            this.phoneRepo.Save(phone);

            MessageHelper message = new MessageHelper(true, "The Phone was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePhone(long id)
        {
            this.phoneRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Phone was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Exams

        public ActionResult ExamDetails(long id = 0, long personId = 0)
        {
            ExamResult exam = this.resultRepo.Get(id);
            if (personId != 0)
                exam = this.GetPerson<ExamResult>(personId);
            
            var modal = exam;

            ViewBag.ListOfExams = this.GetExams(modal.ExamId);
            ViewBag.ListOfAssets = this.GetAssets(modal.AssetId);
            ViewBag.ListOfResults = this.GetLookups("result", modal.ResultLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult SaveExam(ExamResult exam)
        {
            this.resultRepo.Save(exam);

            MessageHelper message = new MessageHelper(true, "The Exam Result was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteExam(long id)
        {
            this.resultRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Exam Result was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Functions

        private IEnumerable<SelectListItem> GetExams(long selectedId)
        {
            this.examRepo = new ExamRepository();
            return this.examRepo
                .GetAll()
                .Select(exam => new SelectListItem
                {
                    Value = exam.Id.ToString(),
                    Text = exam.Name,
                    Selected = (exam.Id == selectedId)
                });
        }

        private IEnumerable<SelectListItem> GetAssets(long? selectedId)
        {
            this.assetRepo = new AssetRepository();
            return this.assetRepo
                .Find(asset => asset.TypeLookup.Name.Equals("Fit Test"))
                .Select(lookup => new SelectListItem
                {
                    Value = lookup.Id.ToString(),
                    Text = lookup.Name,
                    Selected = (lookup.Id == selectedId)
                });
        }

        private IEnumerable<SelectListItem> GetLookups(string lookupType, long? selectedId)
        {
            return this.lookupRepo
                .Find(lookup => lookup.Type.ToLower().Equals(lookupType.ToLower()))
                .Select(lookup => new SelectListItem
                {
                    Value = lookup.Id.ToString(),
                    Text = lookup.Name,
                    Selected = (lookup.Id == selectedId)
                });
        }

        private IEnumerable<SelectListItem> GetAddressTypes(long? selectedId)
        {
            return this.GetLookups("address type", selectedId);
            //return this.lookupRepo
            //    .Find(lookup => lookup.Type.ToLower().Equals("address type"))
            //    .Select(lookup => new SelectListItem
            //    {
            //        Value = lookup.Id.ToString(),
            //        Text = lookup.Name,
            //        Selected = (lookup.Id == selectedId)
            //    });
        }

        private T GetPerson<T>(long personId) where T : class, new()
        {
            T obj = new T();
            Type classType = typeof(T);

            Person person = this.perRepo.Find(pers => pers.Id == personId)
                    .Select(pers => new Person
                    {
                        Id = pers.Id,
                        FirstName = pers.FirstName,
                        MiddleName = pers.MiddleName,
                        LastName = pers.LastName
                    })
                    .SingleOrDefault();

            foreach (PropertyInfo property in classType.GetProperties())
            {
                switch (property.Name)
                {
                    case "PersonId":
                        property.SetValue(obj, personId);
                        break;
                    case "Person":
                        property.SetValue(obj, person);
                        break;
                    default:
                        // do nothing
                        break;
                }
            }

            return obj;
        }

        #endregion
    }
}