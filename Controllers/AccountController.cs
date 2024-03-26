using InstitueProject.Authentication_Models;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstitueProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult RegisterAsAdmin()
        {
            return View("RegisterAsAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterAsAdmin(RegisterUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userVM.UserName,
                    PasswordHash = userVM.PassWord,
                    Adress = userVM.Adress
                };

                IdentityResult createUserResult = await userManager.CreateAsync(user, user.PasswordHash);

                if (createUserResult.Succeeded)
                {
                    //adding to to role before signing in
                   IdentityResult roleResult = await userManager.AddToRoleAsync(user, "Admin");

                    // create cookie
                    await signInManager.SignInAsync(user, isPersistent: false);  // session cookie

                    return RedirectToAction("Index", "Home");

                    // save
                }
                else
                {
                    foreach (var item in createUserResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View("Register");
                }
            }
            else
            {
                return View("Register");
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userVM )
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userVM.UserName,
                    PasswordHash = userVM.PassWord,
                    Adress = userVM.Adress
                    
                };

                IdentityResult createUserResult = await userManager.CreateAsync(user, user.PasswordHash);

                if (createUserResult.Succeeded)
                {
                    //adding to to role before signing in
                    // the default role is guest ... then the admin can change it in the dashboard
                    IdentityResult roleResult = await userManager.AddToRoleAsync(user, userVM.Role);

                    // create cookie
                    await signInManager.SignInAsync(user, isPersistent: false);  // session cookie

                    return RedirectToAction("Index", "Home");

                    // save
                }
                else
                {
                    foreach (var item in createUserResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View("Register");
                }
            }
            else
            {
                return View("Register");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userDB = await userManager.FindByNameAsync(userVM.UserName);

                if (userDB != null)
                {
                    // check password

                    bool isPasswordFound = await userManager.CheckPasswordAsync(userDB, userVM.PassWord);

                    if (isPasswordFound)
                    {
                        //how to read from the claims (short hand prop to reach the name)
                        string name = User.Identity.Name;

                        //how to read from the claims (using static class(ClaimTypes) to provide the namespace name)
                        User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                        // how to add in the claims
                        //add extra info to the claims to store in the cookie  not the data base
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Institue" , "ITI"));

                        //create cookie
                        await signInManager.SignInWithClaimsAsync(userDB , userVM.RememberMe , claims);
                        //await signInManager.SignInAsync(userDB, userVM.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid Account");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}

