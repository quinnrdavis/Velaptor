<!--
    !! NOTE !! - ONLY PROJECT OWNERS AND MAINTAINERS MANAGE PRODUCTION AND PREVIEW RELEASE PULL REQUESTS
    If you have contributions to make, use the "feature-to-develop" pull request template.
-->
<!--suppress HtmlDeprecatedAttribute -->
<h1 style="font-weight:bold" align="center">Preview Feature Pull Request</h1>

<details><summary>📄Description📄</summary>
<!-- Provide a short general summary of your changes in the Title above -->

This pull request is for preview release **_[add version here]_**
</details>


<h2 style="font-weight:bold" align="center">✅Development Checklist✅</h2>

<details open><summary>🌳Branching🌳</summary>

The name of the preview feature branch for this pull request has the following syntax.

Syntax: _preview/feature/&lt;issue-id&gt;-&lt;description&gt;_  
Example: _preview/feature/123-my-preview-change_
- [ ] Yes
- [ ] No

The name of the branch for this pull request is merging into a preview branch with the following syntax.

Syntax: _preview/v&lt;major-number&gt;.&lt;minor-number&gt;.&lt;patch-number&gt;-preview.&lt;prev-number&gt;_  
Example: _preview/v1.2.3-preview.4_
- [ ] Yes
- [ ] No

The preview feature branch for this pull request was created from a preview branch with the following syntax.

Syntax: _preview/feature/&lt;issue-id&gt;-&lt;description&gt;_  
Example: _preview/feature/123-my-preview-change_
- [ ] Yes
- [ ] No
</details>


<details open><summary>🐛Bugs🐛</summary>

Contains Bug Fix(es)
- [ ] Yes
    - [ ] A ![bug-label](https://user-images.githubusercontent.com/85414302/150812958-10b202a8-84ae-45fb-b7cb-7f4fb68e0e8c.png) label has been added to the pull request.
- [ ] No
</details>


<details open><summary>💣Breaking Change(s)💣</summary>

Any changes, including behavioral, that prevent a library user's application from compiling or behaving correctly.
Refer to this [link](https://docs.microsoft.com/en-us/dotnet/core/compatibility/#modifications-to-the-public-contract) for more information.
- [ ] Yes
- [ ] No
</details>


<details open><summary>✨Enhancements✨</summary>

Contains enhancements that add a feature or behavior.
- [ ] Yes
    - [ ] An ![enhancement-label](https://user-images.githubusercontent.com/85414302/150804213-bd043c7b-54d2-4562-ad3f-69a07723a5ef.png) label has been added to the pull request.
- [ ]  No
</details>


<details open><summary>⚙️Workflow (CI/CD) Changes⚙️</summary>

These kinds of changes are only done by the project owner and maintainers.
- [ ] Yes
    - [ ] A ![workflow-label](https://user-images.githubusercontent.com/85414302/150814606-314933ca-86c7-4edb-99cb-62d2198b20d9.png) label has been added to the pull request.
- [ ] No
</details>


<details open><summary>📃Documentation Updates📃</summary>

Contains changes that require documentation updates to code docs or **Velaptor** documentation.
- [ ] Yes
    - [ ] I have updated the documentation accordingly.
    - [ ] A ![documentation-label](https://user-images.githubusercontent.com/85414302/150810133-939e985d-2246-4c77-8c9c-815855da3664.png) label has been added to the pull request.
- [ ] No
</details>


<details open><summary>🧪Unit Testing🧪</summary>

My change requires unit tests to be written.
- [ ] Yes
    - [ ] I have added tests to cover my changes.
- [ ] No
</details>


<details open><summary>🧪Manual Testing🧪</summary>

I have manually tested my changes to the best of my ability.
This can be done by using the included testing application.
- [ ] Yes
- [ ] No
</details>


<h2 style="font-weight:bold" align="center">✅Code Review Checklist✅</h2>

<!-- Go over all the following points, and put an `x` in all the boxes that apply. -->
<!-- If you're unsure about any of these, don't hesitate to ask. We're here to help! -->
- [ ] Pull request title matches the title of the linked issue.
- [ ] The **_[add version here]_** text in the pull request description replaced with the version.
- [ ] Associated issue exists and is linked to this pull request.
    - One issue per pull request.
- [ ] My code follows the coding style of this project.
    - This is enforced by the *.editorconfig* files in the project and displayed as warnings.  If there is an edge case with coding style that should be ignored or changed, reach out and lets discuss it.
- [ ] All tests passed locally.
    - Status checks are put in place to run unit tests every single time a change is pushed to a pull request.  This does not mean that the tests pass in both the local and CI environment.
- [ ] A ![preview-label](https://user-images.githubusercontent.com/85414302/150838564-33f6044b-55f9-4dd9-8783-1d739de9d92f.png) label has been added to the pull request.