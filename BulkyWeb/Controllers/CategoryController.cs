using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!string.IsNullOrEmpty(category.Name) && !category.Name.All(char.IsLetter))
        {
            ModelState.AddModelError("Name", "Category Name must contain letters only");
        }

        if (ModelState.IsValid)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index", "Category");
        }

        return View(category);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!string.IsNullOrEmpty(category.Name) && !category.Name.All(char.IsLetter))
        {
            ModelState.AddModelError("Name", "Category Name must contain letters only");
        }

        if (ModelState.IsValid)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index", "Category");
        }

        return View(category);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        
        if (id == null)
        {
            return NotFound();
        }
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index", "Category");
    }
}