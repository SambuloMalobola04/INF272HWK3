using INF272HWK3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace INF272HWK3.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();



        public ActionResult ChartFromEF()
        {
           

            return View();
        }


        public async Task<ActionResult> Index()
        {
            var viewModel = new HomePViewModel
            {
                Customerss = await db.customers.ToListAsync(),
                Staffss = await db.staffs.Include(s => s.staffs2).Include(s => s.stores).ToListAsync(),
                Productss = await db.products.Include(p => p.brands).Include(p => p.categories).ToListAsync()

            };

            return View(viewModel);
        }

        public async  Task<ActionResult> About()
        {
            var viewModel = new HomePViewModel
            {
                Customerss = await db.customers.ToListAsync(),
                Staffss = await db.staffs.Include(s => s.staffs2).Include(s => s.stores).ToListAsync(),
                Productss = await db.products.Include(p => p.brands).Include(p => p.categories).ToListAsync()

            };

            return View(viewModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> customerIndex()
        {
            return View(await db.customers.ToListAsync());
        }

        // GET: customers/Details/5
        public async Task<ActionResult> customerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customers customers = await db.customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: customers/Create
        public ActionResult customerCreate()
        {
            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> customerCreate([Bind(Include = "customer_id,first_name,last_name,phone,email,street,city,state,zip_code")] customers customers)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customers);
                await db.SaveChangesAsync();
                return RedirectToAction("customerIndex");
            }

            return View(customers);
        }

        // GET: customers/Edit/5
        public async Task<ActionResult> customerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customers customers = await db.customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> customerEdit([Bind(Include = "customer_id,first_name,last_name,phone,email,street,city,state,zip_code")] customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("customerIndex");
            }
            return View(customers);
        }

        // GET: customers/Delete/5
        public async Task<ActionResult> customerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customers customers = await db.customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("customerDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> customerDeleteConfirmed(int id)
        {
            customers customers = await db.customers.FindAsync(id);
            db.customers.Remove(customers);
            await db.SaveChangesAsync();
            return RedirectToAction("customerIndex");
        }


        public async Task<ActionResult> staffsIndex()
        {
            var staffs = db.staffs.Include(s => s.staffs2).Include(s => s.stores);
            return View(await staffs.ToListAsync());
        }

        // GET: staffs/Details/5
        public async Task<ActionResult> staffsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = await db.staffs.FindAsync(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            return View(staffs);
        }

        // GET: staffs/Create
        public ActionResult staffsCreate()
        {
            ViewBag.manager_id = new SelectList(db.staffs, "staff_id", "first_name");
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            return View();
        }

        // POST: staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> staffsCreate([Bind(Include = "staff_id,first_name,last_name,email,phone,active,store_id,manager_id")] staffs staffs)
        {
            if (ModelState.IsValid)
            {
                db.staffs.Add(staffs);
                await db.SaveChangesAsync();
                return RedirectToAction("staffsIndex");
            }

            ViewBag.manager_id = new SelectList(db.staffs, "staff_id", "first_name", staffs.manager_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staffs.store_id);
            return View(staffs);
        }

        // GET: staffs/Edit/5
        public async Task<ActionResult> staffsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = await db.staffs.FindAsync(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            ViewBag.manager_id = new SelectList(db.staffs, "staff_id", "first_name", staffs.manager_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staffs.store_id);
            return View(staffs);
        }

        // POST: staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> staffsEdit([Bind(Include = "staff_id,first_name,last_name,email,phone,active,store_id,manager_id")] staffs staffs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("staffsIndex");
            }
            ViewBag.manager_id = new SelectList(db.staffs, "staff_id", "first_name", staffs.manager_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staffs.store_id);
            return View(staffs);
        }

        // GET: staffs/Delete/5
        public async Task<ActionResult> staffsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staffs staffs = await db.staffs.FindAsync(id);
            if (staffs == null)
            {
                return HttpNotFound();
            }
            return View(staffs);
        }

        // POST: staffs/Delete/5
        [HttpPost, ActionName("staffsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> staffsDeleteConfirmed(int id)
        {
            staffs staffs = await db.staffs.FindAsync(id);
            db.staffs.Remove(staffs);
            await db.SaveChangesAsync();
            return RedirectToAction("staffsIndex");
        }




        public async Task<ActionResult> productIndex()
        {
            var products = db.products.Include(p => p.brands).Include(p => p.categories);
            return View(await products.ToListAsync());
        }

        // GET: products/Details/5
        public async Task<ActionResult> productDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = await db.products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: products/Create
        public ActionResult productCreate()
        {
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> productCreate([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] products products)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("productIndex");
            }

            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // GET: products/Edit/5
        public async Task<ActionResult> productEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = await db.products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> productEdit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("productIndex");
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", products.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // GET: products/Delete/5
        public async Task<ActionResult> productDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = await db.products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("productDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> productDeleteConfirmed(int id)
        {
            products products = await db.products.FindAsync(id);
            db.products.Remove(products);
            await db.SaveChangesAsync();
            return RedirectToAction("productIndex");
        }
    }
}