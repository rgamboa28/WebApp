using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForDemoPurposeOnly.Models;

namespace ForDemoPurposeOnly.Controllers
{
    public class WatchDetailsController : Controller
    {
        private readonly WatchDetailContext _context;

        public WatchDetailsController(WatchDetailContext context)
        {
            _context = context;
        }

        // GET: WatchDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.WatchDetails.ToListAsync());
        }

        // GET: WatchDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchDetails = await _context.WatchDetails
                .FirstOrDefaultAsync(m => m.WatchId == id);
            if (watchDetails == null)
            {
                return NotFound();
            }

            return View(watchDetails);
        }

        // GET: WatchDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WatchDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WatchId,WatchName,SerialNo,Price,WatchWeight,WatchHeight,WatchDiameter,WatchThickness,ShortDesc,Caliber,Movement,Chronograph,Jewel,CaseMaterial,StrapMaterial,FullDesc")] WatchDetails watchDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watchDetails);
        }

        // GET: WatchDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchDetails = await _context.WatchDetails.FindAsync(id);
            if (watchDetails == null)
            {
                return NotFound();
            }
            return View(watchDetails);
        }

        // POST: WatchDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WatchId,WatchName,SerialNo,Price,WatchWeight,WatchHeight,WatchDiameter,WatchThickness,ShortDesc,Caliber,Movement,Chronograph,Jewel,CaseMaterial,StrapMaterial,FullDesc")] WatchDetails watchDetails)
        {
            if (id != watchDetails.WatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchDetailsExists(watchDetails.WatchId))
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
            return View(watchDetails);
        }

        // GET: WatchDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchDetails = await _context.WatchDetails
                .FirstOrDefaultAsync(m => m.WatchId == id);
            if (watchDetails == null)
            {
                return NotFound();
            }

            return View(watchDetails);
        }

        // POST: WatchDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchDetails = await _context.WatchDetails.FindAsync(id);
            _context.WatchDetails.Remove(watchDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchDetailsExists(int id)
        {
            return _context.WatchDetails.Any(e => e.WatchId == id);
        }
    }
}
