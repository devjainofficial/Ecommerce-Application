using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Application.Models;

namespace Ecommerce_Application.Controllers.CRUD
{
    public class ProductCRUDController : Controller
    {
        private readonly DBContext _context;

        public ProductCRUDController(DBContext context)
        {
            _context = context;
        }

        // GET: ProductDetails
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.ProductDetails.Include(p => p.Category);
            return View(await dBContext.ToListAsync());
        }

        // GET: ProductDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDetails = await _context.ProductDetails
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productDetails == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }

        // GET: ProductDetails/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: ProductDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThumbImage,ProductId,ProductName,Price,SellingPrice,Description,BrandName,CategoryId,SKU")] ProductDetails productDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", productDetails.CategoryId);
            return View(productDetails);
        }

        // GET: ProductDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDetails = await _context.ProductDetails.FindAsync(id);
            if (productDetails == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", productDetails.CategoryId);
            return View(productDetails);
        }

        // POST: ProductDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThumbImage,ProductId,ProductName,Price,SellingPrice,Description,BrandName,CategoryId,SKU,ProductUploadTime")] ProductDetails productDetails)
        {
            if (id != productDetails.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*var product = await _context.ProductDetails.FindAsync(id);
                    var UpdateDetails = product.ProductUploadTime;
                    productDetails.ProductUploadTime = UpdateDetails;
                   */
                    _context.Update(productDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDetailsExists(productDetails.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", productDetails.CategoryId);
            return View(productDetails);
        }

        // GET: ProductDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDetails = await _context.ProductDetails
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productDetails == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }

        // POST: ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _context.ProductDetails.FindAsync(id);
            if (productDetails != null)
            {
                _context.ProductDetails.Remove(productDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDetailsExists(int id)
        {
            return _context.ProductDetails.Any(e => e.ProductId == id);
        }
    }
}
