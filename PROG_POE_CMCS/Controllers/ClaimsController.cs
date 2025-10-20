using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PROG_POE_CMCS.Data;
using PROG_POE_CMCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG_POE_CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly PROG_POE_CMCSContext _context;
        private readonly string _uploadPath;
        public ClaimsController(PROG_POE_CMCSContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _uploadPath = Path.Combine(environment.WebRootPath, "uploads");

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        // GET: Claims
        public async Task<IActionResult> Lecturer()
        {
            return View(await _context.Claim.ToListAsync());
        }
        public async Task<IActionResult> Coordinator()
        {
            return View(await _context.Claim.ToListAsync());
        }
        public async Task<IActionResult> Manager()
        {
            return View(await _context.Claim.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim, List<IFormFile> attachments)
        {
            if (!ModelState.IsValid)
                return View(claim);

            _context.Add(claim);
            await _context.SaveChangesAsync();
            claim.Condition = "Getting Reviewed";

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(_uploadPath, file.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var document = new ClaimDocument
                        {
                            ClaimId = claim.Id,
                            FileName = file.FileName,
                            FilePath = "/uploads/" + file.FileName,
                            ContentType = file.ContentType,
                            UploadDate = DateTime.Now
                        };

                        _context.ClaimDocuments.Add(document);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Lecturer));
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,HoursWorked,HourlyRate,Notes,FileName,FilePath,UploadDate,ContentType")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
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
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
                return NotFound();

            claim.Condition = "Approved";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Coordinator));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
                return NotFound();

            claim.Condition = "Verified";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Coordinator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
                return NotFound();

            claim.Condition = "Rejected";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Coordinator));
        }

        private bool ClaimExists(int id)
        {
            return _context.Claim.Any(e => e.Id == id);
        }
    }
}
