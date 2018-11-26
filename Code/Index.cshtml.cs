using HelloPCF.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloPCF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DemoContext _context;

        public IndexModel(DemoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Workshop> Workshops { get; private set; } = new List<Workshop>();

        public async Task OnGet()
        {
            Workshops = await _context.Workshops.ToListAsync();
        }

        public async Task OnPost(string name, bool reset)
        {
            if (!reset)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _context.Workshops.Add(new Workshop { Name = name });

                    await _context.SaveChangesAsync();
                }

                await OnGet();
            }
            else
            {
                var workshops = await _context.Workshops.ToListAsync();

                _context.Workshops.RemoveRange(workshops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
