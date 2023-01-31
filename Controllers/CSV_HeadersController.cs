using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSV_Read.Data;
using CSV_Read.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Security.Cryptography;

namespace CSV_Read.Controllers
{
    public class CSV_HeadersController : Controller
    {
        private readonly CSVContext _context;

        public CSV_HeadersController(CSVContext context)
        {
            _context = context;
        }

        // GET: CSV_Headers
        public async Task<IActionResult> Index()
        {
              return _context.CSV_Headers != null ? 
                          View(await _context.CSV_Headers.ToListAsync()) :
                          Problem("Entity set 'CSVContext.CSV_Headers'  is null.");
        }

        // GET: CSV_Headers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CSV_Headers == null)
            {
                return NotFound();
            }

            var cSV_Headers = await _context.CSV_Headers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSV_Headers == null)
            {
                return NotFound();
            }

            return View(cSV_Headers);
        }

        // GET: CSV_Headers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CSV_Headers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,index,organization,name,website,country,description,founded,industry,numEmp")] CSV_Headers cSV_Headers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSV_Headers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cSV_Headers);
        }

        public  IActionResult NoAsynchCreate([Bind("Id,index,organization,name,website,country,description,founded,industry,numEmp")] CSV_Headers cSV_Headers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSV_Headers);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // *****Start of custom integration*****

        public List<string> fileListConstructor(string fileName)
        {
            var fileContents = System.IO.File.ReadAllLines(@fileName);   //Injests the csv file into a variable
            var readFile = fileContents.Skip(1).ToList();
            return readFile;
        }

        public List<string> LineParser(string line)
        {
            var lineList = line.Split(',');
            return lineList.ToList();
        }

        public CSV_Headers newClass(List<string> readFile)
        {
            try
            {
                return new CSV_Headers
                {
                    index = Convert.ToInt32(readFile[0]),
                    organization = readFile[1],
                    name = readFile[2],
                    website = readFile[3],
                    country = readFile[4],
                    description = readFile[5],
                    founded = Convert.ToInt32(readFile[6]),
                    industry = readFile[7],
                    numEmp = Convert.ToInt32(readFile[8])
                };
            }
            catch(Exception ex) {
                return new CSV_Headers
                {
                    index = 0,
                    organization = "0",
                    name = "0",
                    website = "0",
                    country = "0",
                    description = "0",
                    founded = 0,
                    industry = "0",
                    numEmp = 0
                };
            }
        }

        public void addClassDB(CSV_HeadersController context, CSV_Headers newRow)
        {
            _context.Add(newRow);
        }

        public IActionResult FileToDb(string fileName)
        {
            var li = fileListConstructor(fileName);
            foreach (var line in li)
            {
                var li2 = LineParser(line);
                var dBClass = newClass(li2);
                NoAsynchCreate(dBClass);
            }
            { 
                return View();
            }
        }

        public async Task<IActionResult> FileReadCreate(CSV_Headers cSV_Headers)
        { 
            _context.Add(cSV_Headers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: CSV_Headers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CSV_Headers == null)
            {
                return NotFound();
            }

            var cSV_Headers = await _context.CSV_Headers.FindAsync(id);
            if (cSV_Headers == null)
            {
                return NotFound();
            }
            return View(cSV_Headers);
        }

        // POST: CSV_Headers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,index,organization,name,website,country,description,founded,industry,numEmp")] CSV_Headers cSV_Headers)
        {
            if (id != cSV_Headers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cSV_Headers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSV_HeadersExists(cSV_Headers.Id))
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
            return View(cSV_Headers);
        }

        // GET: CSV_Headers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CSV_Headers == null)
            {
                return NotFound();
            }

            var cSV_Headers = await _context.CSV_Headers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSV_Headers == null)
            {
                return NotFound();
            }

            return View(cSV_Headers);
        }

        // POST: CSV_Headers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CSV_Headers == null)
            {
                return Problem("Entity set 'CSVContext.CSV_Headers'  is null.");
            }
            var cSV_Headers = await _context.CSV_Headers.FindAsync(id);
            if (cSV_Headers != null)
            {
                _context.CSV_Headers.Remove(cSV_Headers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSV_HeadersExists(int id)
        {
          return (_context.CSV_Headers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
